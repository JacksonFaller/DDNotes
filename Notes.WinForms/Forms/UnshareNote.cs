using System;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class UnshareNote : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly BindingList<User> _users = new BindingList<User>();
        private readonly int _noteId;

        public UnshareNote(ServiceClient client, int noteId)
        {
            InitializeComponent();
            _serviceClient = client;
            _noteId = noteId;
            listUsers.DataSource = _users;
            listUsers.DisplayMember = "Name";
        }

        private void btnUnshare_Click(object sender, EventArgs e)
        {
            var selectedUser = _users[listUsers.SelectedIndex];
            _serviceClient.UnshareNote(_noteId, selectedUser.Id);
            MessageBox.Show($"Доступ к заметке был закрыт для пользователя {selectedUser.Name}", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UnshareNote_Load(object sender, EventArgs e)
        {
            foreach (var user in _serviceClient.GetSharedUsers(_noteId))
            {
                _users.Add(user);
            }
        }
    }
}
