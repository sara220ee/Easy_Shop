using System.Windows;

namespace EasyShopWindow
{
    public partial class RegisterWindow : Window
    {
        private RegisterService _registerService;

        public RegisterWindow()
        {
            InitializeComponent();
            _registerService = new RegisterService();
            DataContext = _registerService.NewClient;
        }

        private void ReturnToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            _registerService.NewClient.Password = PasswordBox.Password;

            if (_registerService.ValidateRegistrationFields())
            {
                OperationResult result = _registerService.RegisterNewClient();

                if (result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Inscription", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Erreur d'inscription", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur d'inscription", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}