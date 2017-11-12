using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Notes.WinForms.Forms
{

    internal partial class ShareNote : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _noteId;
        private readonly string _userName;
        public ShareNote(ServiceClient client, int noteId, string userName)
        {
            InitializeComponent();
            _serviceClient = client;
            _noteId = noteId;
            _userName = userName;
        }

        private void btnShareNote_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Необходимо указать имя пользователя", "Внимание", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
            if (txtUserName.Text.Equals(_userName))
            {
                MessageBox.Show("Нельзя поделиться заметкой с самим собой", "Внимание", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }

            try
            {
                var user = _serviceClient.GetUser(txtUserName.Text);
                _serviceClient.ShareNote(_noteId, user.Id);
                MessageBox.Show($"Доступ к заметке был успешно предоставлен пользователю {txtUserName.Text}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
