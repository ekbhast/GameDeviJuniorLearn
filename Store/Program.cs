using System;
using System.Collections.Generic;

namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ProductConfig> products = new List<ProductConfig>
            {
                new ProductConfig("Хлеб", 45),
                new ProductConfig("Молоко", 85),
                new ProductConfig("Яйца", 120),
                new ProductConfig("Сыр", 450),
                new ProductConfig("Куриное филе", 380),
                new ProductConfig("Рис", 110),
                new ProductConfig("Макароны", 75),
                new ProductConfig("Яблоки", 95),
                new ProductConfig("Бананы", 130),
                new ProductConfig("Сахар", 90)
            };

            Store store = new Store(products);
            store.Work();
        }
    }

    class Store
    {
        private int _startBalanceSeller = 0;
        private int _startBalanceBuyer = 10;
        private List<ProductConfig> _products;

        private Seller _seller;
        private Buyer _buyer;

        public Store(List<ProductConfig> products)
        {
            _products = products;
            _seller = new Seller(_startBalanceSeller, _products);
            _buyer = new Buyer(_startBalanceBuyer);
        }

        public void Work()
        {
            Utils utils = new Utils();
            Menu menu = new Menu();

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                _seller.ShowInfo();
                _buyer.ShowInfo();
                menu.Show();

                int command = utils.ReadInt("\nВыберите пункт меню");

                switch (command)
                {
                    case Menu.Buy:
                        Trade();
                        break;

                    case Menu.Exit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню нет");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход");
        }

        public void Trade()
        {
            Console.WriteLine("Какой товар вы хотите купить?:");
            string userInput = Console.ReadLine();

            if (_seller.TryGetProduct(userInput, out Product product))
            {
                if (_buyer.TryBuy(userInput, product))
                {
                    _seller.Sell(userInput);
                    Console.WriteLine("Покупка успешна");
                }
                else
                {
                    Console.WriteLine("Недостаточно денег");
                }
            }
        }
    }

    class User
    {
        protected Dictionary<string, Product> _products;
        protected string Role;

        public int Balance { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Role}:");
            Console.WriteLine($"Баланс: {Balance} руб.\n");
            Console.WriteLine($"Товары:");

            int index = 1;

            foreach (var product in _products)
            {
                Console.WriteLine($"{index} - {product.Key}, {product.Value.Price}");
                index++;
            }

            Console.WriteLine();
        }
    }

    class Seller : User
    {
        public Seller(int balance, List<ProductConfig> products)
        {
            ProductFactory productFactory = new ProductFactory();

            _products = productFactory.Create(products);
            Balance = balance;
            Role = "Продавец";
        }

        public bool TryGetProduct(string productName, out Product product)
        {
            return _products.TryGetValue(productName, out product);
        }

        public void Sell(string productName)
        {
            Balance += _products[productName].Price;
            _products.Remove(productName);
        }
    }

    class Buyer : User
    {
        public Buyer(int balance)
        {
            _products = new Dictionary<string, Product>();
            Balance = balance;
            Role = "Покупателль";
        }

        public bool TryBuy(string productName, Product product)
        {
            if (Balance < product.Price)
            {
                return false;
            }
            else
            {
                Balance -= product.Price;
                _products.Add(product.Name, product);
                return true;
            }
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }

    class ProductFactory
    {
        public Dictionary<string, Product> Create(List<ProductConfig> configs)
        {
            var products = new Dictionary<string, Product>();

            foreach (var config in configs)
            {
                products.Add(
                    config.Name,
                    new Product(config.Name, config.Price)
                );
            }

            return products;
        }
    }

    class ProductConfig
    {
        public ProductConfig(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }

    class Menu
    {
        public const int Buy = 1;
        public const int Exit = 2;

        public void Show()
        {
            Console.WriteLine($"{Buy}. Купить товар");
            Console.WriteLine($"{Exit}. Выход");
        }
    }

    class Utils
    {
        public int ReadInt(string prompt)
        {
            Console.WriteLine(prompt);

            bool isNumber = false;
            int number = 0;

            while (isNumber == false)
            {
                isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber == false)
                {
                    Console.WriteLine("Вы ввели не число.");
                }
            }

            return number;
        }
    }
}
