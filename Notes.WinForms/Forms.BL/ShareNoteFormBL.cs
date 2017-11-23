using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes.WinForms.Forms.BL
{
    class ShareNoteFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _noteId;
        private readonly string _userName;
        public ShareNoteFormBL(ServiceClient client, int noteId, string userName)
        {
            _serviceClient = client;
            _noteId = noteId;
            _userName = userName;
        }

        public void ShareNote(string userName)
        {
            var user = _serviceClient.GetUser(userName);
            _serviceClient.ShareNote(_noteId, user.Id);
        }

        public bool IsSameUser(string userName)
        {
            return userName == _userName;
        }
    }
}
