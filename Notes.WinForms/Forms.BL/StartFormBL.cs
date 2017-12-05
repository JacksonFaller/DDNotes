using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class StartFormBL
    {
        public User CurrentUser { get; set; }
        private readonly ServiceClient _serviceClient;

        public StartFormBL(ServiceClient client)
        {
            _serviceClient = client;
        }

        public void CreateUser(string userName, string userPassword)
        {
            CurrentUser = _serviceClient.CreateUser(new User { Name = userName, Password = userPassword });
        }

        public async Task Login(string userName, string userPassword)
        {
            CurrentUser = await _serviceClient.ValidateUser(new User { Name = userName, Password = userPassword });
        }

        public bool IsInputDataValid(string userName, string userPassword, out string message)
        {
            if (userName.Length == 0)
            {
                message = "Необходимо задать имя пользователя";
                return false;
            }

            if (userPassword.Length == 0)
            {
                message = "Необходимо задать пароль";
                    return false;
            }

            if (userName.Length < 5 || userName.Length > 25)
            {
                message = "Минимальная длина имени - 5, а максимальная - 25 символов";
                return false;
            }

            if (userPassword.Length < 5 || userPassword.Length > 25)
            {
                message = "Минимальная длина пароля - 5, а максимальная - 25 символов";
                return false;
            }
            message = null;
            return true;
        }
    }
}
