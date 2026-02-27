using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Аuto_repair_shop
{
    internal class Program
    {
        private List<Part> parts = new List<Part>()
        {
            new Part("Двигатель", 5000, 1200),
            new Part("Подвеска", 1500, 400),
            new Part("Трансмиссия", 4000, 1000),
            new Part("Тормоза", 800, 300),
            new Part("Рулевое управление", 1200, 350),
            new Part("Колёса", 600, 200),
            new Part("Система охлаждения", 700, 250),
            new Part("Аккумулятор", 300, 100),
            new Part("Выхлопная система", 900, 300),
            new Part("Топливная система", 1100, 400)
        };

        private List<string> carsModels = new List<string>()
        {
            "Toyota Camry",
            "Toyota Corolla",
            "Volkswagen Golf",
            "BMW 3 Series",
            "Mercedes-Benz C-Class",
            "Audi A4",
            "Ford Focus",
            "Honda Civic",
            "Hyundai Elantra",
            "Nissan Qashqai"
        };

        static void Main(string[] args)
        {
            AutoRepair autoRepair = new AutoRepair();
            autoRepair.Work();
        }
    }

    class AutoRepair
    {
        private int _balance = 0;
        private Queue<Car> _cars = new Queue<Car>();
        private List<Part> _partsStorage;

        private const int ExitPrigramCommand = 0;
        private const int AddCommand = 1;
        private const int RepairCommand = 2;

        
        
        public void Work()
        {
            List<Part> parts = new List<Part>()
            {
                new Part("Двигатель", 5000, 1200),
                new Part("Подвеска", 1500, 400),
                new Part("Трансмиссия", 4000, 1000),
                new Part("Тормоза", 800, 300),
                new Part("Рулевое управление", 1200, 350),
                new Part("Колёса", 600, 200),
                new Part("Система охлаждения", 700, 250),
                new Part("Аккумулятор", 300, 100),
                new Part("Выхлопная система", 900, 300),
                new Part("Топливная система", 1100, 400)
            };

        List<string> carsModels = new List<string>()
            {
                "Toyota Camry",
                "Toyota Corolla",
                "Volkswagen Golf",
                "BMW 3 Series",
                "Mercedes-Benz C-Class",
                "Audi A4",
                "Ford Focus",
                "Honda Civic",
                "Hyundai Elantra",
                "Nissan Qashqai"
            };

            CarFactory carFactory = new CarFactory();
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"{ExitPrigramCommand}. Выход из программы");
                Console.WriteLine($"{AddCommand}. Добавить машину в очередь");
                Console.WriteLine($"{RepairCommand}. Ремонтировать следующую машину");
                Console.WriteLine("Машины в очереди:");
                ShowQueue();
                int command = Utils.ReadInt("Выберите команду");

                switch (command)
                {
                    case ExitPrigramCommand:
                        isExit = true;
                        break;

                    case AddCommand:
                        _cars.Enqueue(carFactory.Create(carsModels, parts));
                        break;

                    case RepairCommand:
                        Repair();
                        break;

                    default:
                        Console.WriteLine("Такого пункта в меню нет");
                        break;
                }

                Console.ReadKey();
            }
            
            Console.WriteLine("Выход");
        }

        public void IncreaseBalance(int money)
        {
            _balance += money;
        }

        public void DecreaseBalance(int money) 
        { 
            _balance -= money;
        }

        public void ShowQueue()
        {
            if (_cars.Count == 0)
            {
                Console.WriteLine("В очереди нет машин");
            }
            else
            {
                foreach(Car car in _cars)
                {
                    Console.WriteLine(car.Model);
                }
            }
        }

        public void Repair()
        {
            int exitCommand = 0;
            Car repairCar = _cars.Dequeue();
            bool isEnd = false;

            while(isEnd == false)
            {
                int brokenPartsCount = repairCar.GetBrokenPartsCount();

                Console.Clear();
                Console.WriteLine($"Баланс: {_balance}");
                Console.WriteLine($"{exitCommand}. Завершить ремонт.\n");

                Console.WriteLine($"Ремонт автомобиля:");
                repairCar.Show();
                Console.WriteLine($"Сломанных деталей {brokenPartsCount}");

                int command = Utils.ReadInt("Выбирете деталь которую хотите отремонтировать или завершите ремонт:");

                if (command == exitCommand || brokenPartsCount == 0)
                {
                    Console.WriteLine("Ремонт завершен");
                    isEnd = true;
                }
                else
                {
                    IncreaseBalance(repairCar.GetPart(command - 1).Price);
                    IncreaseBalance(repairCar.GetPart(command - 1).RepairPraice);
                    repairCar.RepairPart(command - 1);
                }
            }
        }
    }

    class Car
    {
        private List<Part> _parts;

        public Car(string model, List<Part> parts)
        {
            Model = model;
            _parts = parts;
        }

        public string Model { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Модель машины: {Model}");
            Console.WriteLine("Детали машины:");

            for(int i = 0; i < _parts.Count; i++ )
            {
                Console.Write($"{i + 1}.");
                _parts[i].Show();
            }
        }

        public void RepairPart(int index)
        {
            _parts[index].Repair();
        }

        public int GetBrokenPartsCount()
        {
            int partsCout = 0;

            foreach(Part part in _parts)
            {
                if (part.IsBroken == true)
                {
                    partsCout++;
                }
            }

            return partsCout;
        }

        public Part GetPart(int index)
        {
            return _parts[index];
        }
    }

    class CarFactory
    {
        public Car Create(List<string> models, List<Part> partsName)
        {
            List<Part> parts = new List<Part>();
            string model = models[Utils.GenerateRandomNumber(0, models.Count)];

            foreach (Part partName in partsName)
            {
                parts.Add(partName.Clone());

            }

            parts[Utils.GenerateRandomNumber(0, parts.Count)].Broke();

            return new Car(model, parts);
        }
    }

    class Part
    {
        public Part (string name, int price, int repairPrice)
        {
            Name = name;
            IsBroken = Utils.GetRandomBoolean();
            Price = price;
            RepairPraice = repairPrice;
        }

        public string Name { get; private set; }
        public bool IsBroken { get; private set; }
        public int Price { get; private set; }
        public int RepairPraice { get; private set; }

        public void Show()
        {
            string statusName = "Рабочая";

            Console.Write($"{Name} : ");

            if (IsBroken == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                statusName = "Сломана ";
                Console.Write($"{statusName}");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write($"цена {Price}, цена ремонта {RepairPraice}\n");
            }
            else 
            {
                Console.WriteLine($"{statusName}");
            }
        }

        public Part Clone()
        {
            return new Part(Name, Price, RepairPraice);
        }

        public void Broke()
        {
            IsBroken = true;
        }

        public void Repair()
        {
            IsBroken = false;
        }
    }

    class Utils
    {
        private static readonly Random s_random = new Random();

        public static bool GetRandomBoolean()
        {
            return s_random.Next(2) > 0;
        }

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
    }
}
