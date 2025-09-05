using System.Windows;
using System.Windows.Controls;
using ClassShop;

namespace EasyShopWindow
{
    public partial class MainWindow : Window
    {
        private MainService _mainService;

        public MainWindow()
        {
            InitializeComponent();
            _mainService = new MainService();
            DataContext = _mainService;
            _mainService.LoadData();
            CategoryFilterComboBox.ItemsSource = _mainService.GetCategories();
            
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                _mainService.Panier.AddProduct(product);
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CartItem item)
            {
                _mainService.Panier.RemoveProduct(item);
            }
        }

        private void ClearCart_Click(object sender, RoutedEventArgs e)
        {
            _mainService.Panier.Clear();
        }

        private void ConfirmPurchase_Click(object sender, RoutedEventArgs e)
        {
            _mainService.ConfirmPurchase();
        }

        private void CategoryFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mainService.FilterProductsByCategory(CategoryFilterComboBox.SelectedItem as Category);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}










