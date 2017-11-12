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
