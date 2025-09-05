
namespace ClassShop
{
    public class Admin : Person
    {
        #region Constructores
        public Admin() : base()
        {
            // Initialisation par défaut
        }
        public Admin(string name, string email, string password, string address, DateTime birthDate , int id) : base(name, email, password, address, birthDate , id )
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
