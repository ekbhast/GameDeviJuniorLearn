namespace WeaponsReport
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullNames = new List<string>
            {
                "Иван Петров",
                "Алексей Смирнов",
                "Дмитрий Иванов",
                "Сергей Кузнецов",
                "Андрей Соколов",
                "Николай Попов",
                "Владимир Лебедев",
                "Павел Морозов",
                "Артём Волков",
                "Максим Фёдоров"
            };

            var ranks = new List<string>
            {
                "Рядовой",
                "Ефрейтор",
                "Сержант",
                "Лейтенант",
                "Капитан",
                "Майор",
                "Подполковник",
                "Полковник",
                "Генерал",
                "Прапорщик"
            };

            var weapons = new List<string>
            {
                "АК-47",
                "М4А1",
                "Пистолет Glock 17",
                "Пистолет Desert Eagle",
                "Снайперская винтовка",
                "Дробовик",
                "Пистолет-пулемёт MP5",
                "РПГ-7",
                "FN SCAR",
                "HK416"
            };

            SoldierFactory factory = new();
            List<Soldier> soldiers = factory.Create(fullNames, weapons, ranks);

            Report report = new(soldiers);
            report.Start();
        }
    }

    class Report
    {
        private List<Soldier> _soldiers = new();

        public Report(List<Soldier> soldiers){
            _soldiers = soldiers.ToList();
        }

        public void Start()
        {
            ShowAllSoldiersData();

            SelectAndShowSoldierData();

            Console.ReadLine();
        }

        public void ShowAllSoldiersData()
        {
            Console.WriteLine("Изначальные данные:\n");

            foreach(var soldier in _soldiers)
            {
                Console.WriteLine($"Имя - {soldier.FullName}\nЗвание - {soldier.Rank}\nОружие - {soldier.Weapon}\nСрок службы - {soldier.TermOfService} месяцец.");
                Console.WriteLine(Utils.EqualSignString);
            }
        }

        public void SelectAndShowSoldierData()
        {
            var soldierData = _soldiers.Select(soldier => new {soldier.FullName, soldier.Rank}).ToList();

            Console.WriteLine("Выбранные данные:\n");

            foreach(var soldier in soldierData)
            {
                Console.WriteLine($"Полное имя - {soldier.FullName}\nЗвание - {soldier.Rank}");
                Console.WriteLine(Utils.EqualSignString);
            }
        }
    }

    class Soldier
    {
        public Soldier(string fullName, string weapon, string rank, int termOfService)
        {
            FullName = fullName;
            Weapon = weapon;
            Rank = rank;
            TermOfService = termOfService;
        }

        public string FullName { get; private set; }
        public string Weapon { get; private set; }
        public string Rank { get; private set; }
        public int TermOfService { get; private set; }
    }

    class SoldierFactory
    {   
        private const int SoldierCount = 10;
        private const int maxTermOfService = 24;

        public List<Soldier> Create(List<string> names, List<string> weapons, List<string> ranks)
        {
            List<Soldier> soldiers = new();

            for(int i = 0; i < SoldierCount; i++)
            {
                string fullName = names[Utils.GenerateRandomNumber(0, names.Count)];
                string weapon = weapons[Utils.GenerateRandomNumber(0, weapons.Count)];
                string rank = ranks[Utils.GenerateRandomNumber(0, ranks.Count)];
                int termOfService = Utils.GenerateRandomNumber(0 , maxTermOfService);

                Soldier soldier = new (fullName, weapon, rank, termOfService);
                soldiers.Add(soldier);
            }

            return soldiers;
        }
    }

     class Utils
    {
        private const int EqualSignCount = 40;
        private static readonly bool[] s_bools = [false, true];

        public static string EqualSignString { get; } = new string('=', EqualSignCount);

        private static readonly Random s_random = new Random();
        
        public static bool GetRandomBoolean()
        {
            return s_bools[s_random.Next(s_bools.Length)];
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
