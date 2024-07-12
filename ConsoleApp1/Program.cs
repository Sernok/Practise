using ConsoleApp1.Domain;
using ConsoleApp1.Domain.Entities;
using ConsoleTableExt;
using ConsoleTables;

namespace ConsoleApp1
{
    public class Program
    {
        static ApplicationContext context = new ApplicationContext();
        static Order currentOrder = null;

        static void Main(string[] args)
        {
            context.LoadData();
            InitializeData();
            context.DisplayProductsInCategory("Каши");

            MainMenu();
            context.SaveData();
        }

        static void InitializeData()
        {
            if (!context.Categories.Any() && !context.Products.Any())
            {
                // Создание категорий
                var categories = new[]
                {
                    new Category(0) { Name = "Блины" },
                    new Category(1) { Name = "Каши" },
                    new Category(2) { Name = "Напитки" },
                    new Category(3) { Name = "Салаты" },
                    new Category(4) { Name = "Десерты" }
                };

                // Создание продуктов
                var products = new[]
                {
                    new Product(0)
                    {
                        Name = "Блин с ветчиной и сыром",
                        Description = "Вкусный блин с начинкой из ветчины и сыра.",
                        Categories = new List<Category> { categories[0] },
                        Protein = 8.0,
                        Fats = 10.0,
                        Carbohydrates = 30.0,
                        Calories = 250.0,
                        Price = 120.0m
                    },
                    new Product(1)
                    {
                        Name = "Блин с творогом и изюмом",
                        Description = "Блин с начинкой из творога и изюма.",
                        Categories = new List<Category> { categories[0] },
                        Protein = 7.0,
                        Fats = 8.0,
                        Carbohydrates = 25.0,
                        Calories = 200.0,
                        Price = 100.0m
                    },
                    new Product(2)
                    {
                        Name = "Овсяная каша",
                        Description = "Полезная овсяная каша с фруктами.",
                        Categories = new List<Category> { categories[1] },
                        Protein = 5.0,
                        Fats = 3.0,
                        Carbohydrates = 20.0,
                        Calories = 150.0,
                        Price = 80.0m
                    },
                    new Product(3)
                    {
                        Name = "Гречневая каша",
                        Description = "Гречневая каша с грибами.",
                        Categories = new List<Category> { categories[1] },
                        Protein = 6.0,
                        Fats = 2.0,
                        Carbohydrates = 18.0,
                        Calories = 140.0,
                        Price = 90.0m
                    },
                    new Product(4)
                    {
                        Name = "Чай черный",
                        Description = "Классический черный чай.",
                        Categories = new List<Category> { categories[2] },
                        Protein = 0.0,
                        Fats = 0.0,
                        Carbohydrates = 0.0,
                        Calories = 0.0,
                        Price = 30.0m
                    },
                    new Product(5)
                    {
                        Name = "Кофе",
                        Description = "Ароматный кофе.",
                        Categories = new List<Category> { categories[2] },
                        Protein = 0.0,
                        Fats = 0.0,
                        Carbohydrates = 0.0,
                        Calories = 5.0,
                        Price = 50.0m
                    },
                    new Product(6)
                    {
                        Name = "Салат Цезарь",
                        Description = "Салат с курицей, листьями салата и пармезаном.",
                        Categories = new List<Category> { categories[3] },
                        Protein = 10.0,
                        Fats = 12.0,
                        Carbohydrates = 5.0,
                        Calories = 180.0,
                        Price = 150.0m
                    },
                    new Product(7)
                    {
                        Name = "Салат Оливье",
                        Description = "Классический салат Оливье.",
                        Categories = new List<Category> { categories[3] },
                        Protein = 8.0,
                        Fats = 15.0,
                        Carbohydrates = 12.0,
                        Calories = 220.0,
                        Price = 130.0m
                    },
                    new Product(8)
                    {
                        Name = "Мороженое",
                        Description = "Ванильное мороженое.",
                        Categories = new List<Category> { categories[4] },
                        Protein = 3.0,
                        Fats = 7.0,
                        Carbohydrates = 20.0,
                        Calories = 180.0,
                        Price = 60.0m
                    },
                    new Product(9)
                    {
                        Name = "Шоколадный торт",
                        Description = "Шоколадный торт с кремом.",
                        Categories = new List<Category> { categories[4] },
                        Protein = 5.0,
                        Fats = 20.0,
                        Carbohydrates = 40.0,
                        Calories = 300.0,
                        Price = 200.0m
                    }
                };

                context.Products.AddRange(products);
                context.Categories.AddRange(categories);


            }
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Терминал для заказа.");
                Console.WriteLine("Введите номер действия.");
                Console.WriteLine("1. В зале");
                Console.WriteLine("2. С собой");
                Console.WriteLine("3. Просмотр заказов");
                Console.WriteLine("[q – завершить работу]");

                string input = Console.ReadLine();
                if (input == "q")
                {
                    context.SaveData();
                    break;
                }

                switch (input)
                {
                    case "1":
                        Console.WriteLine("в зале");
                        StartOrder("в зале");
                        break;
                    case "2":
                        Console.WriteLine("с собой");
                        StartOrder("с собой");
                        break;
                    case "3":
                        Console.WriteLine("просмотр заказов");
                        ViewOrders();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте еще раз.");
                        break;
                }

            }
        }

        static void StartOrder(string place)
        {
            currentOrder = new Order(context.Orders.Count + 1)
            {
                Place = place,
                CreationDate = DateTime.Now,
                OrderNumber = GenerateOrderNumber()
            };
            SelectCategory();
        }

        static void SelectCategory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Терминал для заказа.");
                Console.WriteLine("Введите категорию.");

                for (int i = 0; i < context.Categories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {context.Categories[i].Name}");
                }
                Console.WriteLine("[b – назад, p – оформить заказ, c - отменить заказ, q – завершить работу]");

                string input = Console.ReadLine();

                if (input == "b")
                {
                    return;
                }
                else if (input == "p")
                {
                    ConfirmOrder();
                    return;
                }
                else if (input == "c")
                {
                    CancelOrder();
                    return;
                }
                else if (input == "q")
                {
                    context.SaveData();
                    Environment.Exit(0);
                }

                if (int.TryParse(input, out int categoryIndex) && categoryIndex > 0 && categoryIndex <= context.Categories.Count)
                {
                    SelectProduct(context.Categories[categoryIndex - 1]);
                }
                else
                {
                    Console.WriteLine("Неверный ввод, попробуйте еще раз.");
                }
            }
        }

        static void SelectProduct(Category category)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Терминал для заказа. Категория: {category.Name}");
                Console.WriteLine("Выберите продукт:");

                // Фильтруем продукты по выбранной категории
                var products = context.Products
                    .Where(p => p.Categories.Any(c => c.Id == category.Id))
                    .ToList();

                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i].Name} ({products[i].Price} руб.)");
                }

                Console.WriteLine("[b - назад, p - оформить заказ, c - отменить заказ, q - завершить работу]");

                string input = Console.ReadLine();

                if (input == "b")
                {
                    return;
                }
                else if (input == "p")
                {
                    ConfirmOrder();
                    return;
                }
                else if (input == "c")
                {
                    CancelOrder();
                    return;
                }
                else if (input == "q")
                {
                    context.SaveData();
                    Environment.Exit(0);
                }

                if (int.TryParse(input, out int productIndex) && productIndex > 0 && productIndex <= products.Count)
                {
                    AddProductToOrder(products[productIndex - 1]);
                }
                else
                {
                    Console.WriteLine("Неверный ввод, попробуйте еще раз.");
                }
            }
        }


        static void AddProductToOrder(Product product)
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine($"Введите количество для {product.Name}:");
            Console.WriteLine("[b – назад, p – оформить заказ, c - отменить заказ, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "b")
            {
                return;
            }
            else if (input == "p")
            {
                ConfirmOrder();
                return;
            }
            else if (input == "c")
            {
                CancelOrder();
                return;
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }

            if (int.TryParse(input, out int quantity) && quantity > 0)
            {
                currentOrder.Items.Add(new OrderItem(currentOrder.Items.Count + 1, product, quantity));
                ProductAddedToOrder();
            }
            else
            {
                Console.WriteLine("Неверный ввод, попробуйте еще раз.");
            }
        }

        static void ProductAddedToOrder()
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine("Продукт успешно добавлен в заказ. Введите номер действия.");
            Console.WriteLine("1. Продолжить выбор продуктов");
            Console.WriteLine("[b – назад, p – оформить заказ, c - отменить заказ, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "1")
            {
                SelectCategory();
            }
            else if (input == "b")
            {
                SelectCategory();
            }
            else if (input == "p")
            {
                ConfirmOrder();
            }
            else if (input == "c")
            {
                CancelOrder();
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }
        }

        static void ConfirmOrder()
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine("Ваш заказ сформирован.");
            decimal total = 0;
            foreach (var item in currentOrder.Items)
            {
                decimal itemTotal = item.Product.Price * item.Quantity;
                total += itemTotal;
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = {itemTotal} руб.");
            }
            Console.WriteLine($"Итого = {total} руб.");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("[b – назад, a – подтвердить заказ, c - отменить заказ, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "b")
            {
                SelectCategory();
            }
            else if (input == "a")
            {
                FinalizeOrder();
            }
            else if (input == "c")
            {
                CancelOrder();
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }
        }

        static void FinalizeOrder()
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine("Ваш заказ оформлен!");
            context.Orders.Add(currentOrder);
            currentOrder = null;
            Console.WriteLine("[m – в меню, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "m")
            {
                MainMenu();
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }
        }

        static void CancelOrder()
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine("Ваш заказ отменен!");
            currentOrder = null;
            Console.WriteLine("[m – в меню, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "m")
            {
                MainMenu();
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }
        }

        static void ViewOrders()
        {
            Console.Clear();
            Console.WriteLine("Терминал для заказа.");
            Console.WriteLine("Список заказов:");

            var table = new ConsoleTable("Номер заказа", "Место", "Дата", "Продукты", "Цена");

            foreach (var order in context.Orders)
            {
                var products = string.Join(", ", order.Items.Select(item => $"{item.Product.Name} x {item.Quantity}"));
                table.AddRow(order.OrderNumber, order.Place, order.CreationDate, products,  order.Items.Sum(item => item.Product.Price * item.Quantity));
            }

            table.Configure(o => o.EnableCount = false).Write(Format.Alternative);

            Console.WriteLine("[m – в меню, q – завершить работу]");

            string input = Console.ReadLine();

            if (input == "m")
            {
                MainMenu();
            }
            else if (input == "q")
            {
                context.SaveData();
                Environment.Exit(0);
            }
        }


        static string GenerateOrderNumber()
        {
            // Определение последнего номера заказа и формирование нового
            var lastOrder = context.Orders.OrderByDescending(o => o.Id).FirstOrDefault();
            if (lastOrder != null && lastOrder.OrderNumber != null)
            {
                // Извлекаем последний номер заказа
                string lastOrderNumber = lastOrder.OrderNumber;
                int lastNumber = int.Parse(lastOrderNumber.Split('-')[1]);

                // Инкрементируем номер и форматируем его
                int newNumber = (lastNumber + 1) % 100;
                return $"A-{newNumber:00}";
            }
            else
            {
                return "A-00";
            }
        }
    }
}