using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class UnshareNoteFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly BindingList<User> _users = new BindingList<User>();
        private readonly int _noteId;

        public UnshareNoteFormBL(ServiceClient client, int noteId, BindingList<User> users)
        {
            _serviceClient = client;
            _noteId = noteId;
            _users = users;
            _users.AddRange(_serviceClient.GetSharedUsers(_noteId));
        }

        public User Unshare(int index)
        {
            var selectedUser = _users[index];
            _serviceClient.UnshareNote(_noteId, selectedUser.Id);
            return selectedUser;
        }
    }
}
