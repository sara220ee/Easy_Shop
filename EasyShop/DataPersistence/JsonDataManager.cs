using ClassShop;
using Newtonsoft.Json;

namespace DataPersistence
{
    public static class JsonDataManager
    {
       
        public static void SaveProductsToJson(List<Product> products, string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<Product> LoadProductsFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Product>>(json);
            }
            return new List<Product>();
        }

        public static void AddArticleToJson(Product product, string filePath)
        {
            List<Product> products = LoadProductsFromJson(filePath);
            products.Add(product); 
            SaveProductsToJson(products, filePath);
        }

        public static void RemoveProductFromJson(int productId, string filePath)
        {
            List<Product> products = LoadProductsFromJson(filePath); 
            products.RemoveAll(c => c.Id == productId);
            SaveProductsToJson(products, filePath);
        }

        public static void UpdateProductInJson(Product product, string filePath)
        {
            List<Product> products = LoadProductsFromJson(filePath);
            Product productToUpdate = products.Find(c => c.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;

            }
            SaveProductsToJson(products, filePath);
        }

        public static void SaveCustomersToJson(List<Customer> customers, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                
                File.Create(filePath).Close();
            }
            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<Customer> LoadCustomersFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Customer>>(json);
            }
            return new List<Customer>();
        }

        public static void AddCustomerToJson(Customer customer, string filePath)
        {
            List<Customer> customers = LoadCustomersFromJson(filePath);
            customers.Add(customer);
            SaveCustomersToJson(customers, filePath);
        }

        public static void RemoveCustomerFromJson(int customerId, string filePath)
        {
            List<Customer> customers = LoadCustomersFromJson(filePath);
            customers.RemoveAll(c => c.Id == customerId);
            SaveCustomersToJson(customers, filePath);
        }

        public static void UpdateCustomerInJson(Customer updatedCustomer, string filePath)
        {
            List<Customer> customers = LoadCustomersFromJson(filePath);
            Customer customerToUpdate = customers.Find(c => c.Id == updatedCustomer.Id);
            if (customerToUpdate != null)
            {
                customerToUpdate.Name = updatedCustomer.Name;
                customerToUpdate.Email = updatedCustomer.Email;
                customerToUpdate.Password = updatedCustomer.Password;
                customerToUpdate.BirthDate = updatedCustomer.BirthDate;
            }
            SaveCustomersToJson(customers, filePath);
        }
        public static List<Admin> LoadAdminsFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Admin>>(json);
            }
            return new List<Admin>();
        }
        public static void SaveAdminsToJson(List<Admin> admins, string filePath)
        {
            if (!File.Exists(filePath))
            {
                // Créer le fichier s'il n'existe pas
                File.Create(filePath).Close(); // Fermer le flux pour éviter les erreurs
            }
            string json = JsonConvert.SerializeObject(admins, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        public static void RemoveAdminFromJson(int adminId, string filePath)
        {
            List<Admin> admins = LoadAdminsFromJson(filePath);
            admins.RemoveAll(a => a.Id == adminId);
            SaveAdminsToJson(admins, filePath);
        }
        public static void AddAdminToJson(Admin admin, string filePath)
        {
            List<Admin> admins = LoadAdminsFromJson(filePath);
            admins.Add(admin);
            SaveAdminsToJson(admins, filePath);
        }
        public static void UpdateAdminInJson(Admin updatedAdmin, string filePath)
        {
            List<Admin> admins = LoadAdminsFromJson(filePath);
            Admin adminToUpdate = admins.Find(a => a.Id == updatedAdmin.Id);
            if (adminToUpdate != null)
            {
                adminToUpdate.Name = updatedAdmin.Name;
                adminToUpdate.Email = updatedAdmin.Email;
                adminToUpdate.Password = updatedAdmin.Password;
                adminToUpdate.BirthDate = updatedAdmin.BirthDate;
            }
            SaveAdminsToJson(admins, filePath);
        }
        public static List<Category> LoadCategoriesFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Category>>(json);
            }
            return new List<Category>();
        }
        public static void SaveCategoriesToJson(List<Category> categories, string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        public static void UpdateCategoryInJson(Category updatecategory, string filePath)
        {
            List<Category> categories = LoadCategoriesFromJson(filePath);
            Category categoryToUpdate = categories.Find(a => a.Id == updatecategory.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.NameCat = updatecategory.NameCat;
            }
            SaveCategoriesToJson(categories, filePath);
        }
        public static void AddCategoryToJson(Category category, string filePath)
        {
            List<Category> categories = LoadCategoriesFromJson(filePath);
            categories.Add(category);
            SaveCategoriesToJson(categories, filePath);
        }
        public static void RemovecategoryFromJson(int categoryId, string filePath)
        {
            List<Category> categories = LoadCategoriesFromJson(filePath);
            categories.RemoveAll(a => a.Id == categoryId);
            SaveCategoriesToJson(categories, filePath);
        }

    }

}
