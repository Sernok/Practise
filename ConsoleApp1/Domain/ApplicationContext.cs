using ConsoleApp1.Domain.Entities;
using System.Text.Json;

namespace ConsoleApp1.Domain
{
    public class ApplicationContext
    {
        private const string DataFilePath = "data.json";

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public void LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                string jsonData = File.ReadAllText(DataFilePath);
                var data = JsonSerializer.Deserialize<ApplicationContext>(jsonData);
                if (data != null)
                {
                    Categories = data.Categories;
                    Products = data.Products;
                    Orders = data.Orders;
                }
            }
        }

        public void SaveData()
        {
            string jsonData = JsonSerializer.Serialize(this);
            File.WriteAllText(DataFilePath, jsonData);
        }

        public void DisplayProductsInCategory(string categoryName)
        {
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                var productsInCategory = Products.Where(p => p.Categories.Contains(category)).ToList();
                foreach (var product in productsInCategory)
                {
                    Console.WriteLine($"{product.Name} - {product.Description}");
                }
            }
            else
            {
                Console.WriteLine($"Категория {categoryName} не найдена.");
            }
        }
    }
}
