using System.Security.Cryptography;
using System.Text;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public class RegisterService
    {
        public static string FilePathCustomerJSON = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Customer.json";
        public static string FilePathCustomerXML = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\xml\\Customer.xml";
        public Customer NewClient { get; set; } = new Customer();

        public bool ValidateRegistrationFields()
        {
            return !string.IsNullOrWhiteSpace(NewClient.Name) &&
                   !string.IsNullOrWhiteSpace(NewClient.Address) &&
                   !string.IsNullOrWhiteSpace(NewClient.Email) &&
                   !string.IsNullOrWhiteSpace(NewClient.Password) &&
                   NewClient.BirthDate != default ;
        }


        public OperationResult RegisterNewClient()
        {
            try
            {
                List<Customer> customers = JsonDataManager.LoadCustomersFromJson(FilePathCustomerJSON);

                if (customers != null && customers.Any(c =>
                    string.Equals(c.Email?.Trim(), NewClient.Email?.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new OperationResult(false, "Un compte avec cet email existe déjà.");
                }
                if(NewClient.BirthDate.Date == DateTime.Today)
                {
                    return new OperationResult(false, "Date invalide .");
                }

                Customer customer = new Customer()
                {
                    Id = customers.Count == 0 ? 1 : customers.Max(c => c.Id) + 1,
                    Name = NewClient.Name,
                    Address = NewClient.Address,
                    Email = NewClient.Email,
                    Password = HashPassword(NewClient.Password),
                    BirthDate = NewClient.BirthDate
                };
                JsonDataManager.AddCustomerToJson(customer, FilePathCustomerJSON);
                customers.Add(customer);
                XmlDataManager.SaveCustomersToXml(customers, FilePathCustomerXML);

                return new OperationResult(true, "Inscription réussie ! Vous pouvez maintenant vous connecter.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de l'inscription: {ex.Message}");
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