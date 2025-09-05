using System.Windows;
using System.Windows.Controls;
using AppParamRegistry;

namespace EasyShopWindow
{
        public partial class LoginWindow : Window
        {


            private  LoginService _loginService;
            private  MyAppParamManager _paramManager;

        public LoginWindow()
            {
            _paramManager = new MyAppParamManager();
            _paramManager.LoadRegistryParameters();
            // Initialisation de la fenêtre de connexion
            InitializeComponent();
                _loginService = new LoginService();
                DataContext = _loginService.CurrentPerson;
            if (!string.IsNullOrEmpty(_paramManager.LastUserEmail))
            {
                _loginService.CurrentPerson.Email = _paramManager.LastUserEmail;
                PasswordBox.Focus();
            }
        }
        #region methodes
        private void Login_Click(object sender, RoutedEventArgs e)
            {
                string password = PasswordBox.Password;
                LoginResult result = _loginService.AttemptLogin(password);

                if (result.IsSuccess)
                {
                _paramManager.LastUserEmail = _loginService.CurrentPerson.Email;
                _paramManager.SaveRegistryParameters();
                MessageBox.Show(result.Message, "Connexion", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (result.IsAdmin)
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }
                    else
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Register_Click(object sender, RoutedEventArgs e)
            {
                RegisterWindow registerWindow = new RegisterWindow();
                registerWindow.Show();
                this.Close();
            }
        #endregion
    }
}