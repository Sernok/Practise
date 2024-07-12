using System.Text.Json.Serialization;

namespace ConsoleApp1.Domain.Entities
{
    public class Product
    {
        private static int _nextId = 0; // Статическое поле для хранения следующего идентификатора

        public int Id { get; }  // идентификатор, доступен только для чтения
        public string Name { get; set; }  // название продукта
        public string Description { get; set; }  // описание продукта
        public List<Category> Categories { get; set; }  // список категорий продукта
        public double Protein { get; set; }  // содержание белка на 100 г продукта
        public double Fats { get; set; }  // содержание жиров на 100 г продукта
        public double Carbohydrates { get; set; }  // содержание углеводов на 100 г продукта
        public double Calories { get; set; }  // количество калорий в продукте
        public decimal Price { get; set; }  // цена продукта

        [JsonConstructor]
        public Product(int id, string name, string description, List<Category> categories, double protein, double fats, double carbohydrates, double calories, decimal price) : this(id)
        {
            Name = name;
            Description = description;
            Categories = categories;
            Protein = protein;
            Fats = fats;
            Carbohydrates = carbohydrates;
            Calories = calories;
            Price = price;
        }

        public Product() { }  // конструктор по умолчанию

        public Product(int id)  // конструктор с одним параметром – идентификатором
        {
            Id = id;
        }
        private static int GetNextId()
        {
            return _nextId++;
        }
    }
}
