using System;
using System.Net.Http;
using System.Windows.Forms;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{

    internal partial class ShareNoteForm : Form
    {
        private ShareNoteFormBL _fromBL;

        public ShareNoteForm(ServiceClient client, int noteId, string userName)
        {
            InitializeComponent();
            _fromBL = new ShareNoteFormBL(client, noteId, userName);
        }

        private void btnShareNote_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Необходимо указать имя пользователя", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            if (_fromBL.IsSameUser(txtUserName.Text))
            {
                MessageBox.Show("Нельзя поделиться заметкой с самим собой", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            try
            {
                _fromBL.ShareNote(txtUserName.Text);
                MessageBox.Show($"Доступ к заметке был успешно предоставлен пользователю {txtUserName.Text}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
}
