using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using System.Linq;
using System.Text;

namespace Notes.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly ServiceClient _serviceClient = new ServiceClient("http://localhost:59358/api/");
        private User _user;

        private readonly BindingList<Note> _notes = new BindingList<Note>();
        private readonly BindingList<Note> _sharedNotes = new BindingList<Note>();
        private readonly List<int> _filteredCategoriesIndices = new List<int>();
        private int _dgCategoriesColumnIndex;

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
            _user.Notes = _serviceClient.GetNotes(_user.Id);
            _user.Categories = _serviceClient.GetUserCategories(_user.Id);
            foreach (var note in _user.Notes)
            {
                //note.Categories = _serviceClient.GetNoteCategories(note.Id);
                _notes.Add(note);
            }

            _dgCategoriesColumnIndex = DGListNotes.Columns.Add(new DataGridViewTextBoxColumn());
            DGListNotes.Columns[_dgCategoriesColumnIndex].HeaderText = "Категории";
            DGListNotes.Columns[_dgCategoriesColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < _notes.Count; i++)
            {
                SetCellComboBoxItems(DGListNotes, i, _dgCategoriesColumnIndex, _notes[i].Categories.Select(x => x.Name));
            }
            btnUpdateSharedNotes_Click(sender, e);
            listSharedNotes.ClearSelected();
        }

        private void btnCreateNote_Click(object sender, EventArgs e)
        {
            using (var form = new CreateOrEditNoteForm(_serviceClient, _user))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _notes.Add(_serviceClient.CreateNote(
                        new Note {Title = form.Note.Title, Text = form.Note.Text, Creator = _user.Id}));
                }
            }
        }

        private void btnEditNote_Click(object sender, EventArgs e)
        {
            int selected = DGListNotes.SelectedRows[0].Index;
            using (var form = new CreateOrEditNoteForm(_serviceClient, _user, _notes[selected]))
            {
                form.Text = "Изменить заметку";
                if (form.ShowDialog() != DialogResult.OK) return;

                 var note = _serviceClient.UpdateNote(new Note {Id = _notes[selected].Id, Title = form.Note.Title, Text = form.Note.Text});
                _notes[selected].Title = note.Title;
                _notes[selected].Text = note.Text;
                _notes[selected].ChangingDate = form.Note.ChangingDate;
                _notes[selected].Categories = form.Note.Categories;
                foreach (var category in form.AddedCategories)
                {
                    _serviceClient.AddCategoryToNote(_notes[selected].Id, category.Id);
                }

                foreach (var category in form.RemovedCategories)
                {
                    _serviceClient.RemoveCategoryFromNote(_notes[selected].Id, category.Id);
                }

                SetCellComboBoxItems(DGListNotes, selected, _dgCategoriesColumnIndex, _notes[selected].Categories.Select(x => x.Name));
            }
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            _serviceClient.DeleteNote(_notes[DGListNotes.SelectedRows[0].Index].Id);
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

        private void SetCellComboBoxItems<T>(DataGridView dataGrid, int rowIndex, int colIndex, IEnumerable<T> itemsToAdd)
        {
            DataGridViewTextBoxCell dgvcbc = (DataGridViewTextBoxCell)dataGrid.Rows[rowIndex].Cells[colIndex];

            StringBuilder sb =new StringBuilder();
            foreach (var itemToAdd in itemsToAdd)
            {
                sb.Append($"{itemToAdd.ToString()} ");
            }
            dgvcbc.Value = sb.ToString();
        }

        private void listSharedNotes_DoubleClick(object sender, MouseEventArgs e)
        {
            var index = listSharedNotes.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                new ViewNoteForm(_sharedNotes[index]).ShowDialog();
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

        private void btnViewNote_Click(object sender, EventArgs e)
        {
            if(listSharedNotes.SelectedIndex!= -1)
                new ViewNoteForm(_sharedNotes[listSharedNotes.SelectedIndex]).ShowDialog();
        }
    }
}
