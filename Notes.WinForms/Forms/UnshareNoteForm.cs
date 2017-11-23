using System;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class UnshareNoteForm : Form
    {
        private readonly UnshareNoteFormBL _formBL;

        public UnshareNoteForm(ServiceClient client, int noteId)
        {
            InitializeComponent();
            listUsers.DataSource = new BindingList<User>();
            listUsers.DisplayMember = "Name";
            _formBL = new UnshareNoteFormBL(client, noteId, (BindingList<User>)listUsers.DataSource);
        }

        private void btnUnshare_Click(object sender, EventArgs e)
        {
            var selectedUser = _formBL.Unshare(listUsers.SelectedIndex);
            MessageBox.Show($"Доступ к заметке был закрыт для пользователя {selectedUser.Name}", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
