using System.Windows;
using ClassShop;

namespace EasyShopWindow
{
    public partial class AdminManagement : Window
    {
        private AdminManagementService _adminService;

        public AdminManagement()
        {
            InitializeComponent();
            _adminService = new AdminManagementService();
            DataContext = new Admin();
            LoadAdmins();
        }

        private void LoadAdmins()
        {
            AdminDataGrid.ItemsSource = _adminService.GetAdmins();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            OperationResult result = _adminService.ValidateAdminFields(
                NameTextBox.Text,
                AddressTextBox.Text,
                EmailTextBox.Text,
                PasswordBox.Password,
                BirthDatePicker.SelectedDate);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Champs obligatoires", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_adminService.SelectedAdmin != null)
            {
                OperationResult updateResult = _adminService.UpdateAdmin(
                    NameTextBox.Text,
                    AddressTextBox.Text,
                    EmailTextBox.Text,
                    PasswordBox.Password,
                    BirthDatePicker.SelectedDate);

                if (updateResult.IsSuccess)
                {
                    LoadAdmins();
                    ClearForm();
                }
                MessageBox.Show(updateResult.Message, updateResult.IsSuccess ? "Succès" : "Erreur", MessageBoxButton.OK,
                    updateResult.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);
            }
            else
            {
                OperationResult addResult = _adminService.AddAdmin(
                    NameTextBox.Text,
                    AddressTextBox.Text,
                    EmailTextBox.Text,
                    PasswordBox.Password,
                    BirthDatePicker.SelectedDate);

                if (addResult.IsSuccess)
                {
                    LoadAdmins();
                    ClearForm();
                }
                MessageBox.Show(addResult.Message, addResult.IsSuccess ? "Succès" : "Erreur", MessageBoxButton.OK,
                    addResult.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);
            }
        }

        private void EditAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Admin admin)
            {
                _adminService.SetSelectedAdmin(admin);
                DataContext = _adminService.SelectedAdmin;
            }
        }

        private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Admin admin)
            {
                OperationResult result = _adminService.DeleteAdmin(admin);
                if (result.IsSuccess)
                {
                    LoadAdmins();
                }
                MessageBox.Show(result.Message, result.IsSuccess ? "Succès" : "Erreur", MessageBoxButton.OK,
                    result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            NameTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            BirthDatePicker.SelectedDate = DateTime.Now;
            _adminService.ClearSelectedAdmin();
            DataContext = new Admin();
        }
    }
}