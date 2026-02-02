using System;
using System.Collections.Generic;

namespace Computer_Club
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ComputerClub computerClub = new ComputerClub(8);
            computerClub.Work();
        }
    }

    class ComputerClub
    {
        private int _money = 0;
        private List<Computer> _computers = new List<Computer>();
        public Queue<Client> _cllients = new Queue<Client>();

        public ComputerClub(int computersCount)
        {
            Random random = new Random();

            for (int i = 0; i < computersCount; i++)
            {
                _computers.Add(new Computer(random.Next(5, 15)));
            }

            CreateNewClients(25, random);
        }

        public void CreateNewClients(int count, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                _cllients.Enqueue(new Client(random.Next(100, 251), random));
            }
        }

        public void Work()
        {
            while (_cllients.Count > 0)
            {
                Client newClient = _cllients.Dequeue();
                Console.WriteLine($"Баланс компьютерного клуба {_money}. Ждем ного клиента.");
                Console.WriteLine($"\nУ вас новый клиент, и он хочет купить {newClient.DesiredMinutes} минут");
                ShowAllComputerState();

                Console.WriteLine("\n Вы предлагаете ему кмопьютер под номером:");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int computerNumber))
                {
                    computerNumber -= 1;

                    if (computerNumber >= 0 && computerNumber < _computers.Count)
                    {
                        if (_computers[computerNumber].IsTaken)
                        {
                            Console.WriteLine("Компьютер занят");
                        }
                        else
                        {
                            if (newClient.Checkolvency(_computers[computerNumber]))
                            {
                                Console.WriteLine("Оплатил за комп" + (computerNumber + 1));
                                _money += newClient.Pay();
                                _computers[computerNumber].BecomeTaken(newClient);
                            }
                            else
                            {
                                Console.WriteLine("Не хатило денях");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы сами не знаете за какйо компьютер посадить клиента. Он разозлился и ушел");
                    }
                }
                else
                {
                    CreateNewClients(1, new Random());
                    Console.WriteLine("Неверный ввод! Повторите снова.");
                }
                Console.WriteLine("Нажмите любую клавишу.");

                Console.ReadKey();
                Console.Clear(); 
                SpendOneMinute();
            }
        }

        private void SpendOneMinute()
        {
            foreach (var computer in _computers)
            {
                computer.SpendOneMinute();
            }
        }

        private void ShowAllComputerState()
        {
            Console.WriteLine("\nСписок всех компьютеров");
            for (int i = 0; i < _computers.Count; i++)
            {
                Console.Write(i + 1 + " - "); ;
                _computers[i].ShowState();
            }

            Console.WriteLine("\n");
        }
    }

    class Computer
    {
        private Client _client;
        private int _minutesReamining;
        public bool IsTaken
        {
            get
            {
                return _minutesReamining > 0;
            }
        }
        public int PricePerMinute { get; private set; }

        public Computer(int pricePerMinute)
        {
            PricePerMinute = pricePerMinute;
        }

        public void BecomeTaken(Client client)
        {
            _client = client;
            _minutesReamining = _client.DesiredMinutes;
        }

        public void BecomeEmpty()
        {
            _client = null;
        }

        public void SpendOneMinute()
        {
            _minutesReamining--;
        }

        public void ShowState()
        {
            if (IsTaken)
                Console.WriteLine($"Компьютер занят, осталось минут: {_minutesReamining}");
            else
                Console.WriteLine($"Компьютер свободен, цена за минуту: {PricePerMinute}");
        }
    }

    class Client
    {
        private int _money;
        private int _moneyToPay;
        public int DesiredMinutes { get; private set; }

        public Client(int money, Random random)
        {
            _money = money;
            DesiredMinutes = random.Next(10, 30);
        }

        public bool Checkolvency(Computer computer)
        {
            _moneyToPay = DesiredMinutes * computer.PricePerMinute;

            if (_money >= _moneyToPay)
                return true;
            else
            {
                _moneyToPay = 0;
                return false;
            }
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }
    }
}
