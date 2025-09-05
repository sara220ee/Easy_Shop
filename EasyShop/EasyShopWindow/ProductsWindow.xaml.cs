using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public partial class ProductsWindow : Window
    {
        private ProductService _productService;

        public ObservableCollection<Category> Categories => _productService.Categories;
        public ObservableCollection<Product> Articles => _productService.Articles;

        public ProductsWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            LoadData();
            DataContext = this;
        }

        private void LoadData()
        {
            OperationResult result = _productService.LoadAllData();
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CategoryFilterComboBox.ItemsSource = Categories;
            ArticlesDataGrid.ItemsSource = Articles;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            OperationResult saveResult = _productService.SaveAllChanges();
            if (!saveResult.IsSuccess)
            {
                MessageBox.Show(saveResult.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            AdminWindow mainWindow = new AdminWindow();
            mainWindow.Show();
            this.Close();
        }
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is ComboBox comboBox && comboBox.DataContext is Product product)
            {
                product.CategoryProduct = comboBox.SelectedItem as Category;

                OperationResult result = _productService.SaveAllChanges();
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CategoryFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryFilterComboBox.SelectedItem is Category selectedCategory)
            {
                if (selectedCategory.Id == -1)
                {
                    ArticlesDataGrid.ItemsSource = Articles;
                }
                else
                {
                    List<Product> filteredArticles = _productService.FilterArticlesByCategory(selectedCategory);
                    ArticlesDataGrid.ItemsSource = new ObservableCollection<Product>(filteredArticles);
                }
            }
            else
            {
                ArticlesDataGrid.ItemsSource = Articles;
            }
        }

        private void ArticlesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                OperationResult result = _productService.SaveAllChanges();
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ArticlesDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = _productService.CreateNewProduct();
        }

        private void ArticlesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                OperationResult result = _productService.SaveAllChanges();
                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ArticlesDataGrid.SelectedItem is Product selectedArticle)
            {
                OperationResult result = _productService.DeleteArticle(selectedArticle);
                if (result.IsSuccess)
                {
                    ArticlesDataGrid.ItemsSource = Articles;
                    MessageBox.Show(result.Message, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un article à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}