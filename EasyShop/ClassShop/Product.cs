using System.ComponentModel;


namespace ClassShop
{

    public class Product : IIdentifiable, INotifyPropertyChanged
    {
        #region Propriétés
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        private string name;
        public string imagePath;
        public Category categoryProduct;

        private double price;
        private int quantity;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Le prix doit être un nombre positif.");
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string ImagePath
        {
            get => imagePath;
            set { imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("La quantité invalide.");
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public Category CategoryProduct
        {
            get => categoryProduct;
            set { categoryProduct = value; OnPropertyChanged(nameof(CategoryProduct)); }
        }

        #endregion
        #region Constructeur
        public Product() { }
        public Product(int id , string name, string imagePath, Category categoryProduct, double price, int quantity)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            CategoryProduct = categoryProduct;
            Price = price;
            Quantity = quantity;
        }
        #endregion
        #region Méthodes
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"Article {Name}, Prix: {Price}";
        }
        #endregion
    }
}

