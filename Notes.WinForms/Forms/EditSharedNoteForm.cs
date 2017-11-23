using System;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class EditSharedNoteForm : Form
    {
        private readonly EditSharedNoteFormBL _formBL;
        public EditSharedNoteForm(Note note)
        {
            InitializeComponent();
            listNoteCategories.DataSource = new BindingList<Category>();
            listNoteCategories.DisplayMember = "Name";
            _formBL = new EditSharedNoteFormBL(note, (BindingList<Category>)listNoteCategories.DataSource);
        }

        private void ViewNoteForm_Load(object sender, EventArgs e)
        {
            txtNoteTitle.Text = _formBL.Note.Title;
            txtNoteText.Text = _formBL.Note.Text;
            listNoteCategories.ClearSelected();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (txtNoteTitle.Text.Length == 0 || txtNoteText.Text.Length == 0)
            {
                MessageBox.Show("Поля 'Заголовок' и 'Текст' не могут быть пустыми", "Внимение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Prevents dialog from closing
                DialogResult = DialogResult.None;
                return;
            }
            _formBL.Save(txtNoteTitle.Text, txtNoteText.Text);
           
        }

        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            _formBL.SetNoteTitleChanged();
        }
        private void txtNoteText_TextChanged(object sender, EventArgs e)
        {
            _formBL.SetNoteTextChanged();
        }
    }
}
