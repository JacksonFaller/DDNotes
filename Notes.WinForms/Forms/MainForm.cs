using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using System.Linq;

namespace Notes.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly ServiceClient _serviceClient = new ServiceClient("http://localhost:59358/api/");
        private User _user;
        private readonly BindingList<Note> _notes = new BindingList<Note>();
        private readonly BindingList<Note> _sharedNotes = new BindingList<Note>();
        private readonly List<int> _filteredCategoriesIndices = new List<int>();
        private int _categoriesColumnIndex;

        public MainForm()
        {
            InitializeComponent();
            DGListNotes.DataSource = _notes;
            listSharedNotes.DataSource = _sharedNotes;
            listSharedNotes.DisplayMember = "Title";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide();
            using (var startfrom = new StartForm(_serviceClient))
            {
                if (startfrom.ShowDialog() != DialogResult.OK) Close();
                _user = startfrom.CurrentUser;
            }
            _user.Notes = _serviceClient.GetUserNotes(_user.Id);
            _user.Categories = _serviceClient.GetUserCategories(_user.Id);
            foreach (var note in _user.Notes)
            {
                //note.Categories = _serviceClient.GetNoteCategories(note.Id);
                _notes.Add(note);
            }

            btnUpdateSharedNotes_Click(sender, e);
            listSharedNotes.ClearSelected();
            Show();
        }

        private void btnCreateNote_Click(object sender, EventArgs e)
        {
            using (var form = new CreateOrEditNoteForm(_serviceClient, _user))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _notes.Add(_serviceClient.CreateNote(
                        new Note {Title = form.Note.Title, Text = form.Note.Text, Creator = _user.Id, Categories = Enumerable.Empty<Category>()}));
                }
                if(form.AddedCategories.Count == 0) return;
                foreach (var category in form.AddedCategories)
                {
                    _serviceClient.AddCategoryToNote(_notes[_notes.Count - 1].Id, category.Id);
                }
                _notes.Last().Categories = form.AddedCategories;
                _user.Categories.ToList().AddRange(form.AddedCategories);
            }
            DGListNotes.UpdateCellValue(_categoriesColumnIndex, _notes.Count - 1);
        }

        private void btnEditNote_Click(object sender, EventArgs e)
        {
            int selected = DGListNotes.SelectedRows[0].Index;
            using (var form = new CreateOrEditNoteForm(_serviceClient, _user, _notes[selected]))
            {
                form.Text = "Изменить заметку";
                if (form.ShowDialog() != DialogResult.OK) return;

                if (form.IsNoteTextChanged || form.IsNoteTitleChanged)
                {
                    var note = _serviceClient.UpdateNote(new Note
                    {
                        Id = _notes[selected].Id,
                        Title = form.Note.Title,
                        Text = form.Note.Text
                    });
                    _notes[selected].Title = note.Title;
                    _notes[selected].Text = note.Text;
                    _notes[selected].ChangingDate = note.ChangingDate;
                }
                if (form.AddedCategories.Count == 0 && form.RemovedCategories.Count == 0) return;

                foreach (var category in form.AddedCategories)
                {
                    _serviceClient.AddCategoryToNote(_notes[selected].Id, category.Id);
                }
                _user.Categories.ToList().AddRange(form.AddedCategories);

                foreach (var category in form.RemovedCategories)
                {
                    _serviceClient.RemoveCategoryFromNote(_notes[selected].Id, category.Id);
                }
            }
            DGListNotes.UpdateCellValue(_categoriesColumnIndex, selected);
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            _serviceClient.DeleteNote(_notes[DGListNotes.SelectedRows[0].Index].Id);
            _notes.RemoveAt(DGListNotes.SelectedRows[0].Index);
        }

        private void listNotes_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditNote_Click(sender, e);
        }

        private void listNotes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < DGListNotes.Columns.Count; i++)
            {
                switch (DGListNotes.Columns[i].Name)
                {
                    case "Title":
                        DGListNotes.Columns[i].HeaderText = "Заголовок";
                        break;
                    case "ChangingDate":
                        DGListNotes.Columns[i].HeaderText = "Дата изменения";
                        break;
                    case "CategoriesToString":
                        DGListNotes.Columns[i].HeaderText = "Категории";
                        DGListNotes.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        _categoriesColumnIndex = i;
                        break;
                    default:
                        DGListNotes.Columns[i].Visible = false;
                        break;
                }
            }
        }

        private void btnShareNote_Click(object sender, EventArgs e)
        {
            var index = DGListNotes.SelectedRows[0].Index;
            new ShareNote(_serviceClient, _notes[index].Id, _user.Name).ShowDialog();
            btnUpdateSharedNotes_Click(sender, e);
        }

        private void btnUnshareNote_Click(object sender, EventArgs e)
        {
            var index = DGListNotes.SelectedRows[0].Index;
            new UnshareNote(_serviceClient, _notes[index].Id).ShowDialog();
        }

        private void btnUpdateSharedNotes_Click(object sender, EventArgs e)
        {
            _sharedNotes.Clear();

            foreach (var note in _serviceClient.GetSharedNotes(_user.Id))
            {
                _sharedNotes.Add(note);
            }
        }

        private void listSharedNotes_DoubleClick(object sender, MouseEventArgs e)
        {
            var index = listSharedNotes.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                new EditSharedNoteForm(_sharedNotes[index]).ShowDialog();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {       
            using (var form = new CategoriesFilterForm(_user.Categories,_filteredCategoriesIndices, _serviceClient, _user.Id))
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                if (form.SelectedCategoriestIndices.Count == 0)
                {
                    btnClearFilter_Click(sender, e);
                    return;
                }
                DGListNotes.CurrentCell = null;
                for (int i = 0; i < _notes.Count; i++)
                {
                    DGListNotes.Rows[i].Visible = false;
                    foreach (var index in form.SelectedCategoriestIndices)
                    {
                        if (!_notes[i].Categories.Select(x => x.Id).Contains(form.Categories[index].Id)) continue;
                        DGListNotes.Rows[i].Visible = true;
                        break;
                    }
                }

                _filteredCategoriesIndices.Clear();
                _filteredCategoriesIndices.AddRange(form.SelectedCategoriestIndices);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            _filteredCategoriesIndices.Clear();
            foreach (DataGridViewRow row in DGListNotes.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnOpenSharedNote_Click(object sender, EventArgs e)
        {
            var selected = listSharedNotes.SelectedIndex;
            if (listSharedNotes.SelectedIndex == -1) return;
            using (var form = new EditSharedNoteForm(_sharedNotes[selected]))
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                var note = _serviceClient.UpdateNote(new Note
                {
                    Id = form.Note.Id,
                    Title = form.Note.Title,
                    Text = form.Note.Text
                });

                _sharedNotes[selected].Title = note.Title;
                _sharedNotes[selected].Text = note.Text;
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnShowCategories_Click(object sender, EventArgs e)
        {
            using (var form = new CategoriesForm(_user.Categories, _user.Id, _serviceClient))
            {
                form.ShowDialog();
                _user.Categories = form.Categories;
                var categoriesComparer = new CategoriesComparer();
                if (form.RemovedCategories.Count != 0)
                {
                    for (int i = 0; i < _notes.Count; i++)
                    {
                        var categories = _notes[i].Categories as IList<Category> ?? _notes[i].Categories.ToList();
                        var result = categories.Except(
                            form.RemovedCategories, categoriesComparer).ToList();
                        if (result.Count < categories.Count)
                        {
                            _notes[i].Categories = result;
                            DGListNotes.UpdateCellValue(_categoriesColumnIndex, i);
                        }
                    }
                }
                if (form.UpdatedCategories.Count != 0)
                {
                    bool isUpdated = false;
                    for (int i = 0; i < _notes.Count; i++)
                    {
                        foreach (var category in _notes[i].Categories)
                        {
                            var updCategory = form.UpdatedCategories.Find(x => x.Id == category.Id);
                            if (updCategory != null)
                            {
                                category.Name = updCategory.Name;
                                isUpdated = true;
                            }
                        }
                        if(isUpdated)
                        {
                            DGListNotes.UpdateCellValue(_categoriesColumnIndex, i);
                            isUpdated = false;
                        }
                    }
                }
            }
        }

        public class CategoriesComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                return x?.Id == y?.Id;
            }

            public int GetHashCode(Category category)
            {
                return category.Id.GetHashCode();
            }
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            _notes.Clear();
            _sharedNotes.Clear();
            _user = null;
            _filteredCategoriesIndices.Clear();
            MainForm_Load(sender, e);
        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            _notes.Clear();
            foreach (var note in _serviceClient.GetUserNotes(_user.Id))
            {
                _notes.Add(note);
            }
        }
    }
}

