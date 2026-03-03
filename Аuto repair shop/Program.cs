using System;
using System.Collections.Generic;
using System.Linq;

namespace Аuto_repair_shop
{
    internal class Program
    {
        static void Main(string[] args)
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

            AutoRepair autoRepair = new AutoRepair(carsModels, parts);
            autoRepair.Work();
        }
    }

    class AutoRepair
    {
        private const int Fine = 1000;
        private const int FineForPart = 100;

        private const int AnswerYes = 1;
        private const int AnswerNo = 2;

        private const int ExitCommand = 0;
        private const int AddCommand = 1;
        private const int RepairCommand = 2;

        private int _balance = 0;
        private Queue<Car> _cars = new Queue<Car>();
        private Storage _storage;

        private List<string> _carModels;
        private List<Part> _parts;

        public AutoRepair(List<string> carsModels, List<Part> parts)
        {
            _storage = new Storage(parts);
            _carModels = carsModels;
            _parts = parts;
        }

        public void Work()
        {
            CarFactory carFactory = new CarFactory();
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"{ExitCommand}. Выход из программы");
                Console.WriteLine($"{AddCommand}. Добавить машину в очередь");
                Console.WriteLine($"{RepairCommand}. Ремонтировать следующую машину");

                ShowBalance();
                _storage.Show();
                ShowQueue();

                if (_balance < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Ваш баланс ушел в минус. Игра окончена");
                    isExit = true;
                    continue;
                }

                int command = Utils.ReadInt("\nВыберите команду");

                switch (command)
                {
                    case ExitCommand:
                        isExit = true;
                        break;

                    case AddCommand:
                        _cars.Enqueue(carFactory.Create(_carModels, _parts.ToList()));
                        break;

                    case RepairCommand:
                        TryRepairNextCar();
                        break;

                    default:
                        Console.WriteLine("Такого пункта в меню нет");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход");
        }

        private void IncreaseBalance(int money)
        {
            _balance += money;
        }

        private void DecreaseBalance(int money)
        {
            _balance -= money;
        }

        private Part TryDecreaseStorage(Part part)
        {
            Part newPart = null;

            if (_storage.GetPartsCount(part.Name) == 0)
            {
                return null;
            }
            else
            {
                _storage.DecreasePartCount(part.Name);
                newPart = part.Clone();

                return newPart;
            }
        }

        public void TryRepairNextCar()
        {
            if (_cars.Count == 0) 
            {
                Console.Clear();
                Console.WriteLine("В очереди нет машин.");
            }
            else
            {
                RepairNextCar();
            }
        }

        private void ShowQueue()
        {
            Console.WriteLine("\nМашины в очереди:");

            if (_cars.Count == 0)
            {
                Console.WriteLine("В очереди нет машин");
            }
            else
            {
                foreach (Car car in _cars)
                {
                    Console.WriteLine(car.Model);
                }
            }
        }

        private void ShowBalance()
        {
            Console.WriteLine($"\nБаланс: {_balance}");
        }

        private void RepairNextCar()
        {
            Car repairCar = _cars.Dequeue();

            bool isRepairFinished = false;
            bool hasStartedRepair = false;

            while (isRepairFinished == false)
            {
                if (repairCar.GetBrokenPartsCount() == 0 || _balance < 0)
                {
                    Console.WriteLine("Ремонт завершен");
                    isRepairFinished = true;
                }
                else
                {
                    Console.Clear();
                    ShowBalance();
                    Console.WriteLine($"{ExitCommand}. Завершить ремонт.\n");

                    Console.WriteLine($"Сломанных деталей {repairCar.GetBrokenPartsCount()}");
                    repairCar.Show();

                    int command = Utils.ReadInt("\nВыбирете деталь которую хотите отремонтировать или завершите ремонт:");

                    if (command < 0 || command > repairCar.PartsCount)
                    {
                        Console.WriteLine("Такой детали не существует.");
                        Console.ReadKey();
                    }
                    else
                    {
                        if (command == ExitCommand)
                        {
                            isRepairFinished = CompleteRepair(hasStartedRepair, isRepairFinished, repairCar);

                        }
                        else
                        {
                            hasStartedRepair = true;
                            TryReplacePart(repairCar, command);
                        }
                    }
                }

                Console.WriteLine("Выход из ремонта");
            }
        }

        private void TryReplacePart(Car repairCar, int command)
        {
            Part part = repairCar.GetPart(command - 1);
            Part newPart = TryDecreaseStorage(part);

            if (newPart == null)
            {
                Console.WriteLine("Детали нет на складе, выбирете другую");
                Console.ReadKey();
            }
            else if (part.IsBroken == false)
            {
                Console.WriteLine("Вы поменяли целую деть, оплаты не будет.");
                Console.ReadKey();
            }
            else
            {
                IncreaseBalance(part.Price + part.ReplacePraice);
                repairCar.ReplacePart(command - 1, newPart);
            }
        }

        private bool CompleteRepair(bool hasStartedRepair, bool isRepairFinished, Car repairCar)
        {
            if (hasStartedRepair == false)
            {
                int answer = Utils.ReadInt($"Вы не начали ремонт, вы заплатите штраф в" +
                    $" размере {Fine}, вы уверены? \n {AnswerYes} - да \n {AnswerNo} - нет");

                if (answer == AnswerYes)
                {
                    DecreaseBalance(Fine);
                    return true;
                }
            }
            else
            {
                int answer = Utils.ReadInt($"Вы не завершили ремонт, вы заплатите штраф в рзамере {FineForPart}" +
                    $" за каждую деталь, вы уверены? \n {AnswerYes} - да \n {AnswerNo} - нет");

                if (answer == AnswerYes)
                {
                    DecreaseBalance(FineForPart * repairCar.GetBrokenPartsCount());
                    return true;
                }
            }

            return false;
        }
    }
}

class Storage
{
    private Dictionary<string, int> _partsStorage = new Dictionary<string, int>();

    private int _minPartsCount = 5;
    private int _maxPartsCount = 10;

    public Storage(List<Part> parts)
    {
        foreach (Part part in parts)
        {
            _partsStorage.Add(part.Name, Utils.GenerateRandomNumber(_minPartsCount, _maxPartsCount));
        }
    }

    public int GetPartsCount(string name)
    {
        return _partsStorage[name];
    }

    public void DecreasePartCount(string name)
    {
        _partsStorage[name]--;
    }

    public void Show()
    {
        Console.WriteLine("\nСклад:");

        foreach (string key in _partsStorage.Keys)
        {
            Console.WriteLine($"{key}: {_partsStorage[key]}шт.");
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
    public int PartsCount => _parts.Count;

    public void Show()
    {
        Console.WriteLine($"Модель машины: {Model}");
        Console.WriteLine("Детали машины:");

        for (int i = 0; i < _parts.Count; i++)
        {
            Console.Write($"{i + 1}.");
            _parts[i].Show();
        }
    }

    public void ReplacePart(int index, Part part)
    {
        _parts[index] = part;
    }

    public int GetBrokenPartsCount()
    {
        int partsCout = 0;

        foreach (Part part in _parts)
        {
            if (part.IsBroken)
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
    public Car Create(List<string> models, List<Part> partsList)
    {
        List<Part> parts = new List<Part>();
        string model = models[Utils.GenerateRandomNumber(0, models.Count)];

        foreach (Part part in partsList)
        {
            parts.Add(part.Clone());
        }

        foreach (Part part in parts)
        {
            if (Utils.GetRandomBoolean() == true)
            {
                part.Broke();
            }
        }

        parts[Utils.GenerateRandomNumber(0, parts.Count)].Broke();

        return new Car(model, parts);
    }
}

class Part
{
    public Part(string name, int price, int ReplacePrice, bool isBroken = false)
    {
        Name = name;
        IsBroken = isBroken;
        Price = price;
        ReplacePraice = ReplacePrice;
    }

    public string Name { get; private set; }
    public bool IsBroken { get; private set; }
    public int Price { get; private set; }
    public int ReplacePraice { get; private set; }

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

            Console.Write($"цена {Price}, цена ремонта {ReplacePraice}\n");
        }
        else
        {
            Console.WriteLine($"{statusName}");
        }
    }

    public Part Clone()
    {
        return new Part(Name, Price, ReplacePraice);
    }

    public void Broke()
    {
        IsBroken = true;
    }
}

class Utils
{
    private static readonly Random s_random = new Random();

    public static bool GetRandomBoolean()
    {
        List<bool> bools = new List<bool> { false, true };
        return bools[s_random.Next(bools.Count)];
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
