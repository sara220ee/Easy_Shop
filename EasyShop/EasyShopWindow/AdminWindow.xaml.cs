using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        // Déconnexion
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Fermer la fenêtre d'administration
        }

       
        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            AdminManagement adminManagement = new AdminManagement();
            adminManagement.Show();
            this.Close();
        }
        private void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow viewcategory = new CategoryWindow();
            viewcategory.Show();
            this.Close(); // Fermer la fenêtre d'administration
        }

        // Ajouter une catégorie
        private void ManageArticles_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow product = new ProductsWindow();
            product.Show();
            this.Close();

        }

    }
}
