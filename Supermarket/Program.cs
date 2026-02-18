using System;
using System.Collections.Generic;

namespace Supermarket
{
    class Supermarket
    {
        private List<Product> _products = new List<Product>
        {
            new Product("Хлеб", 50),
            new Product("Молоко", 80),
            new Product("Яйца", 120),
            new Product("Сыр", 350),
            new Product("Сливочное масло", 180),
            new Product("Яблоки", 90),
            new Product("Бананы", 110),
            new Product("Апельсины", 130),
            new Product("Курица", 320),
            new Product("Говядина", 600),

            new Product("Рыба", 450),
            new Product("Рис", 90),
            new Product("Макароны", 75),
            new Product("Картофель", 50),
            new Product("Помидоры", 140),
            new Product("Огурцы", 120),
            new Product("Морковь", 60),
            new Product("Лук", 50),
            new Product("Чеснок", 70),
            new Product("Йогурт", 90),

            new Product("Хлопья", 180),
            new Product("Сахар", 75),
            new Product("Соль", 25),
            new Product("Мука", 70),
            new Product("Подсолнечное масло", 160),
            new Product("Кофе", 350),
            new Product("Чай", 200),
            new Product("Шоколад", 120),
            new Product("Печенье", 140),
            new Product("Сок", 110)
        };
        private Queue<Buyer> _buyers = new Queue<Buyer>();
        private int _balance = 0;

        private const int ServeAll = 1;
        private const int GetNewQueueCommand = 2;
        private const int ShowQueueCommand = 3;
        private const int Exit = 4;

        public void Work()
        {
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine($"{ServeAll}. Обслужить очередь");
                Console.WriteLine($"{GetNewQueueCommand}. Получить новых покупателей(очередь)");
                Console.WriteLine($"{ShowQueueCommand}. Показать очередь");
                Console.WriteLine($"{Exit}. Выход");

                Console.WriteLine($"Баланс магазина {_balance}");

                int command = Utils.ReadInt("Выберете пункт меню:");

                switch (command)
                {
                    case ServeAll:
                        ServeQueue();
                        break;

                    case GetNewQueueCommand:
                        GetNewQueue();
                        break;

                    case ShowQueueCommand:
                        ShowQueue();
                        break;

                    case Exit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Вы выбрали несуществующий пункт меню.");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход.");
            Console.ReadKey();
        }

        private void GetNewQueue()
        {
            BuyerFactory buyerFactory = new BuyerFactory();

            int minCountBoyers = 1;
            int maxCountBoyers = 30;

            int buyersCount = Utils.GenerateRandomNumber(minCountBoyers, maxCountBoyers + 1);

            for (int i = 0; i < buyersCount; i++)
            {
                _buyers.Enqueue(buyerFactory.Create(_products));
            }

            Console.WriteLine("Очередь добавлена");
        }

        private void ShowQueue()
        {
            if (_buyers.Count == 0)
            {
                Console.WriteLine("Покупателей еще нет");
            }
            else
            {
                foreach (Buyer buyer in _buyers)
                {
                    buyer.ShowInfo();
                }
            }
        }

        private void ServeQueue()
        {
            if (_buyers.Count == 0)
            {
                Console.WriteLine("Покупателей в очереди нет. Пригласите покупателей");
            }
            else
            {
                for (int i = 0; i < _buyers.Count; i++)
                {
                    Console.WriteLine(new string('=', 45));

                    Buyer buyer = _buyers.Dequeue();
                    int cartCost;

                    Console.WriteLine("Покупатель на кассе:");
                    buyer.ShowInfo();

                    bool isSold = false;

                    while (isSold == false)
                    {
                        cartCost = buyer.GetCartCost();

                        if (cartCost <= 0)
                        {
                            Console.WriteLine("Ни на что не хватило денег - покупатель ушел");
                            isSold = true;
                        }

                        if (buyer.Balance < cartCost)
                        {
                            buyer.DeleteProductInCart();
                        }
                        else
                        {
                            _balance += cartCost;
                            buyer.BuyProducts();
                            isSold = true;
                        }
                    }

                    Console.WriteLine(new string('-', 45));

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Покупатель сделал покупки:");
                    buyer.ShowInfo();
                    Console.WriteLine($"Стоимость покупок:{buyer.GetBagCost()}");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine(new string('=', 45));
                }
            }
        }
    }

    class Buyer
    {
        private List<Product> _cart = new List<Product>();
        private List<Product> _bag = new List<Product>();

        public Buyer(List<Product> products, int balance)
        {
            _cart = products;
            Balance = balance;
        }

        public int Balance { get; private set; }

        public void ShowInfo()
        {

            Console.WriteLine($"Баланс:{Balance}\n");
            Console.WriteLine("Корзина");
            ShowCart();
            Console.WriteLine();
            Console.WriteLine("Сумка");
            ShowBag();

        }

        public void ShowCart()
        {
            foreach (Product product in _cart)
            {
                product.ShowInfo();
            }
        }

        public void ShowBag()
        {
            foreach (Product product in _bag)
            {
                product.ShowInfo();
            }
        }

        public int GetCartCost()
        {
            int cartCost = 0;

            foreach (Product product in _cart)
            {
                cartCost += product.Price;
            }

            return cartCost;
        }

        public int GetBagCost()
        {
            int bagCost = 0;

            foreach (Product product in _bag)
            {
                bagCost += product.Price;
            }

            return bagCost;
        }

        public void DeleteProductInCart()
        {
            int minRemoveIndex = 0;
            int maxRemoveIndex = _cart.Count;

            if (_cart.Count == 0)
            {
                Console.WriteLine("Ни на что не хватило денег - покупатель ушел");
            }
            else
            {
                _cart.RemoveAt(Utils.GenerateRandomNumber(minRemoveIndex, maxRemoveIndex));
            }
        }

        public void BuyProducts()
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                _bag.Add(_cart[i]);
                Balance -= _cart[i].Price;
            }

            _cart.Clear();
        }
    }

    class BuyerFactory
    {
        public Buyer Create(List<Product> products)
        {
            int minCartCount = 1;
            int maxCartCount = 10;

            int minBalance = 200;
            int maxBalance = 2000;

            int buyersCartCount = Utils.GenerateRandomNumber(minCartCount, maxCartCount + 1);
            int balance = Utils.GenerateRandomNumber(minBalance, maxBalance + 1);

            List<Product> cart = new List<Product>();

            for (int i = 0; i < buyersCartCount; i++)
            {
                cart.Add(products[Utils.GenerateRandomNumber(0, products.Count)]);

            }

            Buyer buyer = new Buyer(cart, balance);

            return buyer;
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

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}: {Price}");
        }
    }

    class Utils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int ReadInt(string prompt = "")
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

        public static void ChangeTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            supermarket.Work();
        }
    }
}
