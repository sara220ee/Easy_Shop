using Microsoft.Win32;

namespace AppParamRegistry
{
    public class MyAppParamManager
    {
        private const string RegistryKeyPath = @"Software\EasyShop\Params";


        // Dernier utilisateur connecté (email seulement)
        public string LastUserEmail { get; set; }

        // Dernière catégorie ajoutée
        public string LastAddedCategory { get; set; }

        public void LoadRegistryParameters()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    LastUserEmail = key.GetValue("LastUserEmail") as string ?? string.Empty;
                    LastAddedCategory = key.GetValue("LastAddedCategory") as string ?? string.Empty;
                }
            }
        }

        public void SaveRegistryParameters()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                key.SetValue("LastUserEmail", LastUserEmail);
                key.SetValue("LastAddedCategory", LastAddedCategory);
            }
        }
    }
}