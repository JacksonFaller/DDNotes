using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class CreateOrEditNoteForm : Form
    {
        public readonly CreateOrEditNoteFormBL FormBL; 
        public CreateOrEditNoteForm(ServiceClient client, User user, Note note)
        {
            InitializeComponent();
            listNoteCategories.DataSource = new BindingList<Category>();
            listNoteCategories.DisplayMember = "Name";
            FormBL = new CreateOrEditNoteFormBL(client, user, note, (BindingList<Category>)listNoteCategories.DataSource);
            txtNoteTitle.Text = note.Title;
            txtNoteText.Text = note.Text;
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
            FormBL.SaveNote(txtNoteTitle.Text, txtNoteText.Text);
        }

        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            FormBL.IsNoteTitleChanged = true;
        }

        private void txtNoteText_TextChanged(object sender, EventArgs e)
        {
            FormBL.IsNoteTextChanged = true;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            FormBL.AddCategory();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            FormBL.DeleteCategory(listNoteCategories.SelectedIndex);
        }
    }
}
