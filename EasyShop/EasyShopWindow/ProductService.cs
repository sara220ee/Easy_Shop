using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClassShop;
using DataPersistence;

namespace EasyShopWindow
{
    public class ProductService
    {
        public static string filePathCategoriesJSON = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Categories.json";
        public static string filePathArticlesJSON = "C:\\Users\\sarar\\Downloads\\C#\\c#\\EasyShop\\json\\Products.json";

        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<Product> Articles { get; private set; }

        public ProductService()
        {
            Categories = new ObservableCollection<Category>();
            Articles = new ObservableCollection<Product>();
        }

        public OperationResult LoadAllData()
        {
            try
            {
                
                List<Category> categories = JsonDataManager.LoadCategoriesFromJson(filePathCategoriesJSON);
                Categories = new ObservableCollection<Category>(categories);
                Categories.Insert(0, new Category { Id = -1, NameCat = "Tous les categories" });

                
                List<Product> articles = JsonDataManager.LoadProductsFromJson(filePathArticlesJSON);
                Articles = new ObservableCollection<Product>(articles);

                return new OperationResult(true, "Données chargées avec succès");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        public List<Product> FilterArticlesByCategory(Category selectedCategory)
        {
            if (selectedCategory.Id == -1)
            {
                return Articles.ToList();
            }

            return Articles.Where(a => a.CategoryProduct != null &&
                                     a.CategoryProduct.Id == selectedCategory.Id).ToList();
        }

        public OperationResult SaveAllChanges()
        {
            try
            {
                
                JsonDataManager.SaveProductsToJson(Articles.ToList(), filePathArticlesJSON);
                return new OperationResult(true, "Modifications sauvegardées avec succès");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de la sauvegarde : {ex.Message}");
            }
        }

        public Product CreateNewProduct()
        {
            int newId = Articles.Count == 0 ? 1 : Articles.Max(a => a.Id) + 1;
            return new Product
            {
                Id = newId,
                CategoryProduct = Categories.FirstOrDefault(c => c.Id != -1)
            };
        }

        public OperationResult DeleteArticle(Product article)
        {
            try
            {
                Articles.Remove(article);
                return SaveAllChanges();
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Erreur lors de la suppression : {ex.Message}");
            }
        }
    }
}































//ValidateProductCategories();
/*private void ValidateProductCategories()
        {
            foreach (Product product in Articles)
            {
                if (product.CategoryProduct == null)
                {
                    product.CategoryProduct = Categories.FirstOrDefault();
                }
                else
                {
                    product.CategoryProduct = Categories.FirstOrDefault(c => c.Id == product.CategoryProduct.Id);
                }
            }
        }*/