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
        private List<Train> _train = new List<Train>();
        private Queue<Passenger> _passengers = new Queue<Passenger>();

        public void Work()
        {
            Menu menu = new Menu();
            Utils utils = new Utils();
            TrainFacatory trainFacatory = new TrainFacatory();
            CarFacatory carFacatory = new CarFacatory(20);
            Train train = trainFacatory.Create();
            bool isExit = false;

            while (isExit == false)
            {
                menu.Show();

                int command = utils.ReadInt("Выберете пункт меню");

                switch (command)
                {
                    case Menu.AddTrain:
                        Console.WriteLine("Ща добавим поезд");
                        _passengers = GetQueue(50);
                        train.Builder(_passengers, carFacatory);
                        train.Show();
                        break;

                    case Menu.Exit:
                        isExit = true;
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход");
        }

        public void ShowTrains()
        {

        }

        public Queue<Passenger> GetQueue(int passengerCount)
        {
            PassengerFactory factory = new PassengerFactory();

            Queue<Passenger> passengers = new Queue<Passenger>();

            for (int i = 0; i < passengerCount; i++)
            {
                passengers.Enqueue(factory.Create());
            }

            return passengers;
        }
    }


    class Train
    {
        private List<Car> _cars = new List<Car>();

        public Train (int id)
        {

            Id = id;
        }

        public int Id { get; private set; }

        public void Builder(Queue<Passenger> passengers, CarFacatory carFactory)
        {
            int seatsPerCar = carFactory.Seats;
            int numberOfCars = (int)Math.Ceiling((double)passengers.Count / seatsPerCar);

            for (int i = 0; i < numberOfCars; i++)
            {
                Car car = carFactory.Create(i + 1);

                while (passengers.Count > 0 && car.TryAddPassenger(passengers.Dequeue()))
                {
                    
                }

                _cars.Add(car);
            }
        }

        public void Show()
        {
            foreach (var car in _cars)
            {
                car.ShowPassengers();
            }
            
        }
    }

    class TrainFacatory
    {
        private int _id;

       public Train Create()
        {
            return new Train(_id++);
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

        public bool TryAddPassenger(Passenger passenger)
        {
            if (_passengers.Count >= Seats)
                return false;

            _passengers.Add(passenger);
            return true;
        }

        public void ShowPassengers()
        {
            Console.WriteLine($"Вагон №{Id}: ");
            foreach (var passenger in _passengers)
            {
                Console.WriteLine($"Пассажир ID - {passenger.Id}");
            }
        }
    }

    class CarFacatory
    {
        

        public CarFacatory(int seats)
        {
            Seats = seats;
        }

        public int Seats { get; private set; }

        public Car Create(int id)
        {
            return new Car(id, Seats);
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
        public const int AddTrain = 1;
        public const int Exit = 2;

        public void Show()
        {
            Console.WriteLine($"{AddTrain}. Добавить новый поезд");
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
