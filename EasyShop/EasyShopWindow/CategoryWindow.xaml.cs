using System.Windows;
using ClassShop;
using AppParamRegistry;

namespace EasyShopWindow
{
    public partial class CategoryWindow : Window
    {
        private CategoryService _categoryService;

        public CategoryWindow()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            DataContext = _categoryService;
            LoadCategories();
            if (!string.IsNullOrEmpty(_categoryService.LastAddedCategory))
            {
                AddCategoryNameTextBox.Text = _categoryService.LastAddedCategory;
            }
        }

        private void LoadCategories()
        {
            _categoryService.LoadCategories();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = AddCategoryNameTextBox.Text.Trim();
            OperationResult result = _categoryService.AddCategory(categoryName);

            if (result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                AddCategoryNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(DeleteCategoryIdTextBox.Text.Trim(), out int categoryId))
            {
                MessageBox.Show("Veuillez entrer un ID valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OperationResult result = _categoryService.DeleteCategory(categoryId);
            if (result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                DeleteCategoryIdTextBox.Clear();
            }
            else
            {
                MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
    }
}