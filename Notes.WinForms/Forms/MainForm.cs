using System;
using System.Data;
using System.Windows.Forms;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly MainFormBL _formBL;
        private int _categoriesColumnIndex;
        public MainForm()
        {
            InitializeComponent();
            _formBL = new MainFormBL();
            DGListNotes.DataSource = _formBL.Notes;
            listSharedNotes.DataSource = _formBL.SharedNotes;
            listSharedNotes.DisplayMember = "Title";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {   
            Hide();
            using (var startfrom = new StartForm(_formBL.ServiceClient))
            {
                if (startfrom.ShowDialog() != DialogResult.OK) Close();
                _formBL.Initialize(startfrom.FormBL.CurrentUser);
            }
            btnUpdateSharedNotes_Click(sender, e);
            listSharedNotes.ClearSelected();
            Show();
        }

        private void btnCreateNote_Click(object sender, EventArgs e)
        {
            using (var form = new CreateOrEditNoteForm(_formBL.ServiceClient, _formBL.User))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _formBL.CreateNote(form.FormBL.Note.Title, form.FormBL.Note.Text);
                }
                _formBL.AddCategoriesToNewNote(form.FormBL.AddedCategories);
            }
            DGListNotes.UpdateCellValue(_categoriesColumnIndex, _formBL.Notes.Count - 1);
        }

        private void btnEditNote_Click(object sender, EventArgs e)
        {
            int selected = DGListNotes.SelectedRows[0].Index;
            using (var form = new CreateOrEditNoteForm(_formBL.ServiceClient, _formBL.User, _formBL.Notes[selected]))
            {
                form.Text = "Изменить заметку";
                if (form.ShowDialog() != DialogResult.OK) return;

                if (form.FormBL.IsNoteTextChanged || form.FormBL.IsNoteTitleChanged)
                {
                    _formBL.EditNote(form.FormBL.Note.Title, form.FormBL.Note.Text, selected);
                }
                if (form.FormBL.AddedCategories.Count == 0 && form.FormBL.RemovedCategories.Count == 0) return;
                _formBL.AddCategoriesToNote(form.FormBL.AddedCategories, selected);
                _formBL.RemoveCategoriesFromNote(form.FormBL.RemovedCategories, selected);
            }
            DGListNotes.UpdateCellValue(_categoriesColumnIndex, selected);
            //UpdateDGRow(selected);
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            _formBL.DeleteNote(DGListNotes.SelectedRows[0].Index);
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
            new ShareNoteForm(_formBL.ServiceClient, _formBL.Notes[index].Id, _formBL.User.Name).ShowDialog();
        }

        private void btnUpdateSharedNotes_Click(object sender, EventArgs e)
        {
            _formBL.UpdateSharedNotes();
        }

        private void listSharedNotes_DoubleClick(object sender, MouseEventArgs e)
        {
            var index = listSharedNotes.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                btnOpenSharedNote_Click(sender, e);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {       
            using (var form = new CategoriesFilterForm(_formBL.User.Categories, _formBL.FilteredCategoriesIndices, 
                _formBL.ServiceClient, _formBL.User.Id))
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                if (form.FormBL.SelectedCategoriestIndices.Count == 0)
                {
                    btnClearFilter_Click(sender, e);
                    return;
                }
                DGListNotes.CurrentCell = null;

                for (int i = 0; i < _formBL.Notes.Count; i++)
                {
                    DGListNotes.Rows[i].Visible = false;
                }
                foreach (var category in _formBL.Filter(form.FormBL.SelectedCategoriestIndices, form.FormBL.Categories))
                {
                    DGListNotes.Rows[category].Visible = true;
                }
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            _formBL.ClearFilter();
            foreach (DataGridViewRow row in DGListNotes.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnOpenSharedNote_Click(object sender, EventArgs e)
        {
            var selected = listSharedNotes.SelectedIndex;
            if (listSharedNotes.SelectedIndex == -1) return;
            using (var form = new EditSharedNoteForm(_formBL.SharedNotes[selected]))
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                _formBL.UpdateSharedNote(selected);
            }
        }

        private void btnShowCategories_Click(object sender, EventArgs e)
        {
            using (var form = new CategoriesForm(_formBL.User.Categories, _formBL.User.Id, _formBL.ServiceClient))
            {
                form.ShowDialog();
                _formBL.UpdateUserCategories(form.FormBL.Categories); 
                foreach (var category in _formBL.DeleteCategories(form.FormBL.RemovedCategories))
                {
                    DGListNotes.UpdateCellValue(_categoriesColumnIndex, category);
                }

                foreach (var category in _formBL.UpdateCategories(form.FormBL.UpdatedCategories))
                {
                    DGListNotes.UpdateCellValue(_categoriesColumnIndex, category);
                }
            }
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            _formBL.ChangeUser();
            MainForm_Load(sender, e);
        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            _formBL.UpdateNotes();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateDGRow(int rowIndex)
        {
            for (int i = 0; i < DGListNotes.ColumnCount; i++)
            {
                DGListNotes.UpdateCellValue(i, rowIndex);
            }
        }
    }
}
