namespace TopPlayers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            var names = new List<string>
            {
                "Иванов Иван Иванович",
                "Петров Пётр Сергеевич",
                "Сидоров Алексей Дмитриевич",
                "Кузнецов Андрей Олегович",
                "Смирнов Максим Викторович",
                "Попов Артём Игоревич",
                "Васильев Даниил Романович",
                "Новиков Кирилл Андреевич",
                "Фёдоров Егор Павлович",
                "Морозов Никита Александрович",
                "Волков Тимофей Ильич",
                "Алексеев Роман Денисович",
                "Лебедев Владислав Евгеньевич",
                "Семёнов Глеб Константинович",
                "Егоров Степан Михайлович"
            };   

            Server server = new(names);
            server.Start();         
        }
    }

    class Server
    {
        private List<Player> _players = new();

        public Server(List<string> names)
        {
            PlayerFactory playerFactory = new();
            _players = playerFactory.Create(names);
        }

        public void Start()
        {
            Console.Clear();

            Console.WriteLine("Все игроки сервера:");
            Console.WriteLine(new string('=', 40));
            ShowPlayers(_players);
            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nТоп 3 игрока по уровню:\n");
            ShowTopLevelPlayers();

            Console.WriteLine(new string('*', 40));

            Console.WriteLine("\nТоп 3 игрока по силе:\n");
            ShowTopPowerPlayers();

            Console.ReadKey();
        }

        public void ShowPlayers(List<Player> players)
        {
            foreach(var player in players)
            {
                Console.WriteLine($"Имя - {player.FullName}\nУровень - {player.Level}\nСила - {player.Power}");
                Console.WriteLine(new string('-', 30));
            }
        }

        public void ShowTopLevelPlayers()
        {
            int topPlaces = 3;
            List<Player> topPlayers = _players.OrderByDescending(player => player.Level).Take(topPlaces).ToList();

            ShowPlayers(topPlayers);
        }

            public void ShowTopPowerPlayers()
        {
            int topPlaces = 3;
            List<Player> topPlayers = _players.OrderByDescending(player => player.Power).Take(topPlaces).ToList();

            ShowPlayers(topPlayers);
        }
    }

    class Player
    {
        public string FullName { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public Player( string fullName, int level, int power)
        {
            FullName = fullName;
            Level = level;
            Power = power;
        }
    }

    class PlayerFactory
    {
        private int _playersCount = 10;
        private int _maxLevel = 100;
        private int _maxPower = 100;

        public List<Player> Create(List<string> names)
        {
            List<Player> players = new();

            for (int i = 0; i < _playersCount; i++)
            {
                string name = names[Utils.GenerateRandomNumber(0, names.Count)];
                int level = Utils.GenerateRandomNumber(0, _maxLevel + 1);
                int power = Utils.GenerateRandomNumber(0, _maxPower + 1);
                Player player = new (name, level, power);

                players.Add(player);
            }

            return players;
        }
    }

    class Utils
    {
        private static readonly Random s_random = new Random();

        public static bool GetRandomBoolean()
        {
            return s_random.Next(2) == 0;
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