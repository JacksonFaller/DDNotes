using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class StartForm : Form
    {
        public User CurrentUser { get; set; }
        private readonly ServiceClient _serviceClient;
        public StartForm(ServiceClient client)
        {
            InitializeComponent();
            _serviceClient = client;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if(!IsInputDataValid()) return;
            try
            {
                CurrentUser = _serviceClient.CreateUser(
                    new User {Name = userControl.UserName, Password = userControl.UserPassword});
                MessageBox.Show($"Id пользователя: {CurrentUser.Id}", "Пользователь",
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!IsInputDataValid()) return;
            try
            {
                CurrentUser = _serviceClient.ValidateUser(
                    new User {Name = userControl.UserName, Password = userControl.UserPassword});
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsInputDataValid()
        {
            if (userControl.UserName == string.Empty)
            {
                MessageBox.Show("Необходимо задать имя пользователя", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (userControl.UserPassword == string.Empty)
            {
                MessageBox.Show("Необходимо задать пароль", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (userControl.UserName.Length < 5 || userControl.UserName.Length > 25)
            {
                MessageBox.Show("Минимальная длина имени - 5, а максимальная - 25 символов", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (userControl.UserPassword.Length < 5 || userControl.UserPassword.Length > 25)
            {
                MessageBox.Show("Минимальная длина пароля - 5, а максимальная - 25 символов", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
