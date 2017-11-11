using System;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    public partial class ViewNoteForm : Form
    {
        private readonly Note _note;
        private readonly BindingList<Category> _categories = new BindingList<Category>();
        public ViewNoteForm(Note note)
        {
            InitializeComponent();
            _note = note;
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
            txtNoteTitle.Text = _note.Title;
            txtNoteText.Text = _note.Text;
            listNoteCategories.ClearSelected();
            txtNoteTitle.SelectionStart = txtNoteTitle.Text.Length;
        }
    }
}
