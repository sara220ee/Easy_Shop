using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public class AdminManagementService : INotifyPropertyChanged
    {
        public static string FilePathAdminJSON = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Admin.json";
        public static string FilePathAdminXML = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\xml\\Admin.xml";

        private ObservableCollection<Admin> _admins;
        public ObservableCollection<Admin> Admins
        {
            get => _admins;
            set
            {
                _admins = value;
                OnPropertyChanged(nameof(Admins));
            }
        }

        public Admin SelectedAdmin { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AdminManagementService()
        {
            List<Admin> loadedAdmins = JsonDataManager.LoadAdminsFromJson(FilePathAdminJSON) ?? new List<Admin>();
            Admins = new ObservableCollection<Admin>(loadedAdmins);
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return Admins;
        }

        public void SetSelectedAdmin(Admin admin)
        {
            SelectedAdmin = admin;
            OnPropertyChanged(nameof(SelectedAdmin));
        }

        public void ClearSelectedAdmin()
        {
            SelectedAdmin = null;
            OnPropertyChanged(nameof(SelectedAdmin));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OperationResult ValidateAdminFields(string name, string address, string email, string password, DateTime? birthDate)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) ||
                birthDate == null)
            {
                return new OperationResult(false, "Veuillez remplir tous les champs.");
            }
            if(birthDate.Value.Date == DateTime.Today)
            {
                return new OperationResult(false, "Date invalide.");
            }

            if (SelectedAdmin == null && string.IsNullOrWhiteSpace(password))
            {
                return new OperationResult(false, "Veuillez saisir un mot de passe.");
            }

            return new OperationResult(true, string.Empty);
        }

        public OperationResult AddAdmin(string name, string address, string email, string password, DateTime? birthDate)
        {
            try
            {
                if (Admins.Any(a => a.Email.Equals(email)))
                {
                    return new OperationResult(false, "Un administrateur avec cet email existe déjà.");
                }

                Admin newAdmin = new Admin()
                {
                    Id = Admins.Count == 0 ? 1 : Admins.Max(a => a.Id) + 1,
                    Name = name,
                    Address = address,
                    Email = email,
                    Password = HashPassword(password),
                    BirthDate = birthDate ?? DateTime.Now
                };

                Admins.Add(newAdmin);
                JsonDataManager.AddAdminToJson(newAdmin, FilePathAdminJSON);
                XmlDataManager.SaveAdminsToXml(Admins.ToList(), FilePathAdminXML);

                return new OperationResult(true, "Administrateur ajouté avec succès.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de l'ajout: {ex.Message}");
            }
        }

        public OperationResult UpdateAdmin(string name, string address, string email, string password, DateTime? birthDate)
        {
            try
            {
                if (SelectedAdmin == null)
                {
                    return new OperationResult(false, "Aucun administrateur sélectionné.");
                }

                SelectedAdmin.Name = name;
                SelectedAdmin.Address = address;
                SelectedAdmin.Email = email;
                if (!string.IsNullOrWhiteSpace(password))
                {
                    SelectedAdmin.Password = HashPassword(password);
                }
                SelectedAdmin.BirthDate = birthDate ?? DateTime.Now;

                JsonDataManager.SaveAdminsToJson(Admins.ToList(), FilePathAdminJSON);
                XmlDataManager.SaveAdminsToXml(Admins.ToList(), FilePathAdminXML);

                return new OperationResult(true, "Administrateur mis à jour avec succès.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de la mise à jour: {ex.Message}");
            }
        }

        public OperationResult DeleteAdmin(Admin admin)
        {
            try
            {
                Admins.Remove(admin);
                JsonDataManager.SaveAdminsToJson(Admins.ToList(), FilePathAdminJSON);
                XmlDataManager.SaveAdminsToXml(Admins.ToList(), FilePathAdminXML);
                return new OperationResult(true, "Administrateur supprimé avec succès.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de la suppression: {ex.Message}");
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