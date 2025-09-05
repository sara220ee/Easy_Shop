using System.ComponentModel;

namespace ClassShop
{
    public class Category : IIdentifiable, IComparable<Category>, INotifyPropertyChanged
    {
        #region propriétés
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        private string nameCat;
        public string NameCat
        {
            get => nameCat;
            set
            {
                if (nameCat != value)
                {
                    nameCat = value;
                    OnPropertyChanged(nameof(NameCat));
                }
            }
        }
        #endregion
        #region Constructores
        public Category()
        {
        }
        public Category(int id, string nameCat)
        {
            Id = id;
            NameCat = nameCat;
        }
        #endregion
        #region Méthodes
        public override string ToString()
        {
            return $"{NameCat}";
        }
      
        public int CompareTo(Category other)
        {
            if (other == null) return 1;
            return string.Compare(NameCat, other.NameCat, StringComparison.OrdinalIgnoreCase);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
