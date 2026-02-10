using System;
using System.Collections.Generic;

namespace Train_dispatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.Work();
        }
    }

    class Dispatcher
    {
        private Queue<Passenger> _passengers = new Queue<Passenger>();
        private List<Train> _trains = new List<Train>();

        public void Work()
        {
            Utils utils = new Utils();
            Menu menu = new Menu();
            TrainFactory trainFactory = new TrainFactory();

            Random random = new Random();
            int minRandomValue = 1;
            int maxRandomValue = 100;

            int seatsInCar = 30;

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                menu.Show();

                Console.WriteLine("Инофрмация о пездах:");
                ShowTrains();

                Console.WriteLine();
                int command = utils.ReadInt("Выберете пункт меню");

                switch (command)
                {
                    case Menu.AddCommand:
                        AddTrain(trainFactory, random.Next(minRandomValue, maxRandomValue + 1), seatsInCar);
                        break;

                    case Menu.ExitCommand:
                    isExit = true;
                    break;

                    default:
                        Console.WriteLine("Такого пункта меню не существует.");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход.");
        }

        public Queue<Passenger> GetNewQueue(int count)
        {
            Queue<Passenger> passengers = new Queue<Passenger>();
            PassengerFactory passengerFactory = new PassengerFactory();

            for (int i = 0; i < count; i++)
            {
                passengers.Enqueue(passengerFactory.Create());
            }

            return passengers;
        }

        //Метод ни где не участвует, создан для проверки заполнения очереди.
        public void ShowQueue()
        {
            foreach(Passenger passenger in _passengers)
            {
                Console.WriteLine(passenger.Id);
            }
        }

        public void AddTrain(TrainFactory trainFactory, int queueCount, int seats)
        {
            //Из задания не понятно как должно формироваться направление.
            //представим что они лежат где-то.
            string[] routes = new string[]
            {
                "Москва – Санкт-Петербург",
                "Москва – Екатеринбург",
                "Москва – Нижний Новгород",
                "Санкт-Петербург – Казань",
                "Москва – Новосибирск",
                "Екатеринбург – Челябинск",
                "Москва – Ростов-на-Дону",
                "Санкт-Петербург – Вологда",
                "Москва – Сочи",
                "Казань – Уфа"
            };

            Random random = new Random();
            string route = routes[random.Next(routes.Length)];

            _passengers = GetNewQueue(queueCount);

            Train train = trainFactory.Create(route, _passengers.Count);
            train.AddCars(_passengers, seats);
            _trains.Add(train);

            Console.WriteLine($"Пезд создан, Id: {train.Id}\nСостоит из:");
            train.ShowCars();
        }

        public void ShowTrains()
        {
            Console.WriteLine();

            if (_trains.Count == 0)
            {
                Console.WriteLine("Ни одного поезда не создано");
            }
            else
            {
                foreach (Train train in _trains)
                {
                    Console.WriteLine($"ID поезда - {train.Id} направления {train.Route}," +
                        $" в нем {train.CountCars} вагона(-ов)" +
                        $" и {train.PassangersCount} пассажиров.");
                }
            }
        }

        //метод для тестироания создания и заполнения поездов
        public void ShowAllInfo()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Ни одного поезда не создано");
            }
            else
            {
                foreach (Train train in _trains)
                {
                    Console.Write("\n");
                    Console.WriteLine($"ID поезда - {train.Id}");
                    train.ShowCars();
                }
            }
        }
    }

    class Train
    {
        private List<Car> _cars = new List<Car>();

        public Train(int id, string route, int passangersCount) 
        {
            Id = id; 
            Route = route;
            PassangersCount = passangersCount;
        }

        public int Id { get; private set; }
        public string Route { get; private set; }
        public int PassangersCount { get; private set; }
        public int CountCars { get { return _cars.Count; } }

        public void AddCars(Queue<Passenger> passengers, int seats)
        {
            CarFactory carFactory = new CarFactory();

            int numberOfCars = (passengers.Count + seats - 1) / seats;

            for (int i = 0; i < numberOfCars; i++)
            {
                Car car = carFactory.Create(seats);

                for (int j = 0; j < seats && passengers.Count > 0; j++)
                {
                    car.AddPassenger(passengers.Dequeue());
                }

                _cars.Add(car);
            }
        }

        public void ShowCars()
        {
            foreach (Car car in _cars)
            {
                Console.WriteLine($"Id вагона - {car.Id}");
                car.ShowPassengers();
            }
        }
    }

    class TrainFactory
    {
        private int _id = 0;

        public Train Create(string route, int passangersCount)
        {
            return new Train(_id++, route, passangersCount);
        }
    }

    class Car
    {
        private List<Passenger> _passengers = new List<Passenger>();

        public Car(int id, int seats)
        {
            Id = id;
            Seats = seats;
        }

        public int Id { get; private set; }
        public int Seats { get; private set; }

        public void AddPassenger(Passenger passenger)
        {
           _passengers.Add(passenger);
         }

        public void ShowPassengers()
        {
            foreach (Passenger passenger in _passengers)
            {
                Console.WriteLine($"Id пассажира - {passenger.Id}");
            }
        }
    }

    class CarFactory
    {
        private int _id = 0;
        public int Seats { get; private set; }

        public Car Create(int seats)
        {
            return new Car(_id++, seats);
        }
    }

    class Passenger
    {
        public Passenger(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    class PassengerFactory
    {
        private int _id = 0;

        public Passenger Create()
        {
            return new Passenger(_id++);
        }
    }

    class Menu
    {
        public const int AddCommand = 1;
        public const int ExitCommand = 2;

        public void Show()
        {
            Console.WriteLine($"{AddCommand}. Добавить поезд");
            Console.WriteLine($"{ExitCommand}. Выход");
        }
    }

    class Utils
    {
        public int ReadInt (string prompt)
        {
            Console.WriteLine(prompt);

            bool isNumber = false;
            int number = 0;

            while(isNumber == false)
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
