using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ClassShop;

namespace EasyShopWindow
{
    public class MainService : INotifyPropertyChanged
    {
        private ProductService _productService;
        private ObservableCollection<Product> _filteredProducts;

        public event PropertyChangedEventHandler PropertyChanged;
        public PanierManager Panier { get; } = new PanierManager();

        public ObservableCollection<Product> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }

        public MainService()
        {
            _productService = new ProductService();
        }

        public void LoadData()
        {
            OperationResult result = _productService.LoadAllData();
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            FilteredProducts = new ObservableCollection<Product>(_productService.Articles);
        }

        public void ConfirmPurchase()
        {
            if (Panier.Items.Count == 0)
            {
                MessageBox.Show("Votre panier est vide.", "Panier vide", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                foreach (CartItem item in Panier.Items)
                {
                    Product product = _productService.Articles.First(p => p.Id == item.Product.Id);
                    product.Quantity -= item.Quantity;
                }

                OperationResult result = _productService.SaveAllChanges();
                if (result.IsSuccess)
                {
                    MessageBox.Show("Achat confirmé !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    Panier.Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show(result.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void FilterProductsByCategory(Category selectedCategory)
        {
            if (selectedCategory != null)
            {
                FilteredProducts = new ObservableCollection<Product>(_productService.FilterArticlesByCategory(selectedCategory));
            }
            else
            {
                FilteredProducts = new ObservableCollection<Product>(_productService.Articles);
            }
        }

        public ObservableCollection<Category> GetCategories()
        {
            return _productService.Categories;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}