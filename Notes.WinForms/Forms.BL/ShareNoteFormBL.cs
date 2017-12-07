using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class ShareNoteFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly BindingList<User> _users = new BindingList<User>();
        private readonly int _noteId;
        private readonly string _userName;

        public ShareNoteFormBL(ServiceClient client, int noteId, string userName, BindingList<User> users)
        {
            _serviceClient = client;
            _noteId = noteId;
            _userName = userName;
            _users = users;
            _users.AddRange(_serviceClient.GetSharedUsers(_noteId));
        }

        public User Unshare(int index)
        {
            var user = _users[index];
            _serviceClient.UnshareNote(_noteId, user.Id);
            _users.Remove(user);
            return user;
        }

        public void ShareNote(string userName)
        {
            if (userName == _userName)
                throw new ArgumentException($"Невозможно поделиться заметкой с текущим пользователем: {userName}");
            var user = _serviceClient.GetUser(userName);
            _serviceClient.ShareNote(_noteId, user.Id);
            _users.Add(user);
        }
    }
}