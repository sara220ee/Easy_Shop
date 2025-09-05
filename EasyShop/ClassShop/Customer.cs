
namespace ClassShop
{
    public class Customer : Person
    {
        #region Constructores
        public Customer() : base()
        {
            // Initialisation par défaut
        }
        public Customer(string name, string email, string password, string address, DateTime birthDate, int id) : base(name, email, password, address, birthDate, id)
        {
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"Client Nom: {Name}, Email: {Email}]";
        }
        #endregion
    }
}
