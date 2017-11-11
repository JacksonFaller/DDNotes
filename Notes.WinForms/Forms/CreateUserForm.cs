using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes.WinForms
{
    public partial class CreateUserForm : Form
    {
        public CreateUserForm()
        {
            InitializeComponent();
        }

        public string UserName => _userControl.UserName;
        public string UserPassword => _userControl.UserPassword;
    }
}
