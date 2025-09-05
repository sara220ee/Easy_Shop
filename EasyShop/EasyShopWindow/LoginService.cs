using System.Security.Cryptography;
using System.Text;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public class LoginService
    {
        public static string FilePathCustomer = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Customer.json";
        public static string FilePathAdmin = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Admin.json";
        public Person CurrentPerson { get; private set; } = new Person();

        public LoginResult AttemptLogin(string password)
        {
            if (string.IsNullOrWhiteSpace(CurrentPerson.Email) || string.IsNullOrWhiteSpace(password))
            {
                return new LoginResult(false, "Veuillez remplir tous les champs.");
            }

            try
            {
                // Vérifier si l'utilisateur est l'administrateur
                List<Admin> admins = JsonDataManager.LoadAdminsFromJson(FilePathAdmin);
                Admin admin = admins.Find(c => c.Email == CurrentPerson.Email && c.Password == HashPassword(password));

                if (admin != null)
                {
                    return new LoginResult(true, "Connexion Admin réussie !", true);
                }

                // Vérifier si l'utilisateur est un client
                List<Customer> customers = JsonDataManager.LoadCustomersFromJson(FilePathCustomer);
                Customer customer = customers.Find(c => c.Email == CurrentPerson.Email && c.Password == HashPassword(password));

                if (customer != null)
                {
                    return new LoginResult(true, "Connexion réussie !");
                }

                return new LoginResult(false, "Email ou mot de passe incorrect.");
            }
            catch (Exception ex)
            {
                return new LoginResult(false, $"Une erreur s'est produite lors de la lecture des données : {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}