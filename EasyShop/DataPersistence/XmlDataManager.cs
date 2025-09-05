using System.Xml.Serialization;
using ClassShop;

namespace DataPersistence
{
    public static class XmlDataManager
    {
        public static void SaveProductsToXml(List<Product> products, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, products);
            }
        }

        public static List<Product> LoadProductsFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (List<Product>)serializer.Deserialize(reader);
                }
            }
            return new List<Product>();
        }
        public static void SaveCustomersToXml(List<Customer> customers, string filePathCustomer)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers), "La liste des clients ne peut pas être nulle.");
            }

            if (string.IsNullOrWhiteSpace(filePathCustomer))
            {
                throw new ArgumentException("Le chemin du fichier ne peut pas être vide ou null.", nameof(filePathCustomer));
            }

            try
            {
                string directory = Path.GetDirectoryName(filePathCustomer);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (!File.Exists(filePathCustomer))
                {
                    File.Create(filePathCustomer).Close();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
                using (StreamWriter writer = new StreamWriter(filePathCustomer))
                {
                    serializer.Serialize(writer, customers);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Une erreur s'est produite lors de la sauvegarde des clients en XML.", ex);
            }
        }

        public static List<Customer> LoadCustomersFromXml(string filePathCustomer)
        {
            if (File.Exists(filePathCustomer))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
                using (StreamReader reader = new StreamReader(filePathCustomer))
                {
                    return (List<Customer>)serializer.Deserialize(reader);
                }
            }
            return new List<Customer>();
        }

        public static void SaveCommandesToXml(List<CartItem> orders, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<CartItem>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, orders);
            }
        }

        public static List<CartItem> LoadCommandesFromXml(string filePathCommande)
        {
            if (File.Exists(filePathCommande))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CartItem>));
                using (StreamReader reader = new StreamReader(filePathCommande))
                {
                    return (List<CartItem>)serializer.Deserialize(reader);
                }
            }
            return new List<CartItem>();
        }
        public static void SaveAdminsToXml(List<Admin> admins, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Admin>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, admins);
            }
        }

        public static List<Admin> LoadAdminsFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Admin>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (List<Admin>)serializer.Deserialize(reader);
                }
            }
            return new List<Admin>();
        } 
        public static void SaveCategoriesToXml(List<Category> categories , string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Créer le fichier s'il n'existe pas
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Category>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, categories);
            }
        }

        public static List<Category> LoadCategoriessFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Category>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (List<Category>)serializer.Deserialize(reader);
                }
            }
            return new List<Category>();
        }

    }

}