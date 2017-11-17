using System;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    public partial class EditSharedNoteForm : Form
    {
        public readonly Note Note;
        private readonly BindingList<Category> _categories = new BindingList<Category>();
        private bool _isNoteTextChanged;
        private bool _isNoteTitleChanged;
        public EditSharedNoteForm(Note note)
        {
            InitializeComponent();
            this.Note = note;
            listNoteCategories.DataSource = _categories;
            listNoteCategories.DisplayMember = "Name";
            foreach (var category in note.Categories)
            {
                _categories.Add(category);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewNoteForm_Load(object sender, EventArgs e)
        {
            txtNoteTitle.Text = Note.Title;
            txtNoteText.Text = Note.Text;
            listNoteCategories.ClearSelected();
            txtNoteTitle.SelectionStart = txtNoteTitle.Text.Length;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_isNoteTitleChanged && !_isNoteTextChanged) return;
            if (txtNoteTitle.Text.Length == 0 || txtNoteText.Text.Length == 0)
            {
                MessageBox.Show("Поля 'Заголовок' и 'Текст' не могут быть пустыми", "Внимение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Prevents dialog from closing
                DialogResult = DialogResult.None;
                return;
            }
            Note.Title = _isNoteTitleChanged ? txtNoteTitle.Text : null;
            Note.Text = _isNoteTextChanged ? txtNoteText.Text : null;
        }
    }
}
