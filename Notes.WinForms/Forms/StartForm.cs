using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class StartForm : Form
    {
        public readonly StartFormBL FormBL;
        public StartForm(ServiceClient client)
        {
            InitializeComponent();
            FormBL = new StartFormBL(client);
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string message;
            if (!FormBL.IsInputDataValid(userControl.UserName, userControl.UserPassword, out message))
            {
                MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
               FormBL.CreateUser(userControl.UserName, userControl.UserPassword);
                MessageBox.Show($"Id пользователя: {FormBL.CurrentUser.Id}", "Пользователь",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AggregateException ex)
            {
                Exception exception = ex.InnerException;
                var messageBuilder = new StringBuilder($"{exception.Message}{Environment.NewLine}");

                while(exception.InnerException!= null)
                {
                    exception = exception.InnerException;
                    messageBuilder.Append($"<-{exception.Message}{Environment.NewLine}");
                }
                MessageBox.Show(messageBuilder.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*var result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                FormClosed += (o, args) => Application.Exit();
            else
                e.Cancel = true;*/
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string message;
            if (!FormBL.IsInputDataValid(userControl.UserName, userControl.UserPassword, out message))
            {
                MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnCreateUser.Enabled = false;
            btnLogin.Enabled = false;
            try
            {
                await FormBL.Login(userControl.UserName, userControl.UserPassword);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
