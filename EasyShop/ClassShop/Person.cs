using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassShop
{
    public class Person : IIdentifiable , INotifyPropertyChanged
    {
        #region Fields and properties
        public int Id { get; set; }
        private string name { get; set; }
        private string email { get; set; }
        private string password { get; set; }
        private string address { get; set; }
        private DateTime birthDate;
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("La date de naissance ne peut pas être dans le futur.");

                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name)); 
                }
            }
        }
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }
        #endregion
        #region Constructors
        public Person()
        {
            Id = 0;
            Name = "";
            Email = "";
            Password = "";
            Address = "";
            BirthDate = DateTime.Now;
        }
        public Person(string name, string email, string password, string address, DateTime birthDate , int id)
        {
            Id = id ;
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            BirthDate = birthDate;
        }
        #endregion
        #region methods
        public override string ToString()
        {
            return $"personne Nom: {Name}, Email: {Email}, Adresse: {Address}, Date de naissance: {BirthDate.ToShortDateString()}";
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}