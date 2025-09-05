using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ClassShop;

namespace EasyShopWindow
{
    public class PanierManager : INotifyPropertyChanged
    {
        private ObservableCollection<CartItem> _items = new ObservableCollection<CartItem>();

        public ObservableCollection<CartItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(Total));
            }
        }

        public decimal Total => Items.Sum(item => (decimal)(item.Product.Price * item.Quantity));

        public void AddProduct(Product product)
        {
            CartItem existingItem = Items.FirstOrDefault(item => item.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = 1 });
            }

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Total));
        }

        public void RemoveProduct(CartItem item)
        {
            Items.Remove(item);
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Total));
        }

        public void Clear()
        {
            Items.Clear();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Total));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}