using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class CreateOrEditNoteForm : Form
    {
        public Note Note { get; set; }
        public List<Category> AddedCategories = new List<Category>();
        public List<Category> RemovedCategories = new List<Category>();

        public bool IsNoteTextChanged;
        public bool IsNoteTitleChanged;

        private readonly ServiceClient _serviceClient;
        private readonly User _user;
        private readonly BindingList<Category> _categories = new BindingList<Category>();

        public CreateOrEditNoteForm(ServiceClient client, User user, Note note)
        {
            InitializeComponent();
            _user = user;
            _serviceClient = client;
            txtNoteTitle.Text = note.Title;
            txtNoteText.Text = note.Text;
            Note = note;
            if(note.Categories == null) return;
            foreach (var category in note.Categories)
            {
                _categories.Add(category);
            }
        }
        public CreateOrEditNoteForm(ServiceClient client, User user): this(client, user, new Note())
        {
        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            if (txtNoteTitle.Text.Length == 0 || txtNoteText.Text.Length == 0)
            {
                MessageBox.Show("Поля 'Заголовок' и 'Текст' не могут быть пустыми", "Внимение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Prevents dialog from closing
                DialogResult = DialogResult.None;
                return;
            }
            if (!IsNoteTitleChanged && !IsNoteTextChanged) return;
            Note.Title = IsNoteTitleChanged ? txtNoteTitle.Text : null;
            Note.Text = IsNoteTextChanged ? txtNoteText.Text : null;
            Note.Categories = _categories;
        }

        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            IsNoteTitleChanged = true;
        }

        private void txtNoteText_TextChanged(object sender, EventArgs e)
        {
            IsNoteTextChanged = true;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (var form = new AddCategoryForm(_categories, _serviceClient, _user))
            {
                if (form.ShowDialog() != DialogResult.OK || form.SelectedCategoriesIndices.Count == 0) return;
                foreach (var index in form.SelectedCategoriesIndices)
                {
                    _categories.Add(form.Categories[index]);
                    AddedCategories.Add(form.Categories[index]);
                }
            }
        }

        private void CreateOrEditNoteForm_Load(object sender, EventArgs e)
        {
            listNoteCategories.DataSource = _categories;
            listNoteCategories.DisplayMember = "Name";
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (_categories.Count == 0) return;
            var index = listNoteCategories.SelectedIndex;
            if (AddedCategories.Contains(_categories[index]))
            {
                AddedCategories.Remove(_categories[index]);
                _categories.RemoveAt(index);
                return;
            }
            RemovedCategories.Add(_categories[index]);
            _categories.RemoveAt(index);
        }
    }
}
