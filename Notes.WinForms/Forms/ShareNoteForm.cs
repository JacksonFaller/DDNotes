using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class ShareNoteForm : Form
    {
        private ShareNoteFormBL _formBL;

        public ShareNoteForm(ServiceClient client, int noteId, string userName)
        {
            InitializeComponent();
            listUsers.DataSource = new BindingList<User>();
            listUsers.DisplayMember = "Name";
            _formBL = new ShareNoteFormBL(client, noteId, userName, (BindingList<User>)listUsers.DataSource);
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Необходимо указать имя пользователя", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _formBL.ShareNote(txtUserName.Text);
                MessageBox.Show($"Доступ к заметке был успешно предоставлен пользователю {txtUserName.Text}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnshare_Click(object sender, EventArgs e)
        {
            var selectedUser = _formBL.Unshare(listUsers.SelectedIndex);
            MessageBox.Show($"Доступ к заметке был закрыт для пользователя {selectedUser.Name}", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
