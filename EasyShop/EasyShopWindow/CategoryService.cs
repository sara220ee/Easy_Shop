using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ClassShop;
using DataPersistence;
using AppParamRegistry;

namespace EasyShopWindow
{
    public class CategoryService : INotifyPropertyChanged
    {
        public static string FilePathCategoriesJSON = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Categories.json";
        public static string FilePathCategoriesXML = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\xml\\Categories.xml";
        private readonly MyAppParamManager _paramManager;
        public string LastAddedCategory
        {
            get => _paramManager.LastAddedCategory;
            set
            {
                _paramManager.LastAddedCategory = value;
                _paramManager.SaveRegistryParameters();
            }
        }
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CategoryService()
        {
            _paramManager = new MyAppParamManager();
            _paramManager.LoadRegistryParameters();
            Categories = new ObservableCollection<Category>();
            Categories.CollectionChanged += Categories_CollectionChanged;
        }

        private void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Category category in e.NewItems)
                {
                    category.PropertyChanged += Category_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (Category category in e.OldItems)
                {
                    category.PropertyChanged -= Category_PropertyChanged;
                }
            }
            SaveCategories();
        }

        private void Category_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCategories();
        }

        public void LoadCategories()
        {
            try
            {
                List<Category> categories = JsonDataManager.LoadCategoriesFromJson(FilePathCategoriesJSON);
                if (categories != null)
                {
                    categories.Sort();
                }

                Categories.Clear();
                foreach (Category category in categories ?? new List<Category>())
                {
                    Categories.Add(category);
                }

                if (Categories.Count == 0)
                {
                    MessageBox.Show("Aucune catégorie trouvée.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des catégories : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public OperationResult AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return new OperationResult(false, "Veuillez entrer un nom de catégorie.");
            }

            try
            {
                int newId = Categories.Count == 0 ? 1 : Categories.Max(c => c.Id) + 1;
                Category newCategory = new Category { Id = newId, NameCat = categoryName };
                Categories.Add(newCategory);
                _paramManager.LastAddedCategory = categoryName;
                _paramManager.SaveRegistryParameters();

                return new OperationResult(true, "Catégorie ajoutée avec succès !");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        public OperationResult DeleteCategory(int categoryId)
        {
            try
            {
                Category categoryToRemove = Categories.FirstOrDefault(c => c.Id == categoryId);
                if (categoryToRemove == null)
                {
                    return new OperationResult(false, "Aucune catégorie trouvée avec cet ID.");
                }

                Categories.Remove(categoryToRemove);
                return new OperationResult(true, "Catégorie supprimée avec succès !");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        private void SaveCategories()
        {
            try
            {
                JsonDataManager.SaveCategoriesToJson(Categories.ToList(), FilePathCategoriesJSON);
                XmlDataManager.SaveCategoriesToXml(Categories.ToList(), FilePathCategoriesXML);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}