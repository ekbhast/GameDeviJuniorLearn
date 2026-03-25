namespace ExpirationDate
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>
            {
                "Тушёнка говяжья",
                "Тушёнка свиная",
                "Тушёнка куриная",
                "Тушёнка из индейки",
                "Тушёнка говяжья высший сорт",
                "Тушёнка свиная с пряностями",
                "Тушёнка куриная с овощами",
                "Тушёнка из баранины",
                "Тушёнка домашняя",
                "Тушёнка армейская"
            };

            CannedMeatFactory factory = new();
            List<CannedMeat> cannedMeats = factory.Create(names);

            Warehouse warehouse = new(cannedMeats);
            warehouse.Start();
        }
    }

    class Warehouse
    {
        private List<CannedMeat> _cannedMeats = new();

        public Warehouse(List<CannedMeat> cannedMeats)
        {
            _cannedMeats = cannedMeats.ToList();
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Все продукты:");
            ShowCannedMeat(_cannedMeats);   
            Console.WriteLine(Utils.EqualSignString);

            Console.WriteLine("\nПросроченные продукты\n");
            ShowExpiredProduct();
            Console.ReadKey();
        }

        public void ShowCannedMeat(List<CannedMeat> cannedMeats)
        {
            foreach(var meat in cannedMeats)
            {
                Console.WriteLine($"Наименование - {meat.Name}\nГод выпуска - {meat.Year}\nСрок годности - {meat.ExpirationDate}");
                Console.WriteLine(Utils.EqualSignString);
            }
        }

        public void ShowExpiredProduct()
        {
            int currentYear = DateTime.Now.Year;

            List<CannedMeat> expiredProduct = _cannedMeats.Where(meat => meat.Year + meat.ExpirationDate < currentYear).ToList();

            ShowCannedMeat(expiredProduct);
        }
    }

    class CannedMeat
    {
        public CannedMeat(string name, int year, int expirationDate)
        {
            Name = name;
            Year = year;
            ExpirationDate = expirationDate;
        }

        public string Name { get; private set; }
        public int Year { get; private set; }
        public int ExpirationDate{ get; private set;}
    }

    class CannedMeatFactory
    {
        private int _cannedMeatCount = 10; 
        private int _minYear = 2010;
        private int _maxYear = 2026;
        private int _minExpirationDate = 1;
        private int _maxExpirationDate = 10;

        public List <CannedMeat> Create(List<string> names)
        {
            List<CannedMeat> cannedMeats = new();

            for (int i = 0; i < _cannedMeatCount; i++)
            {
                string name = names[Utils.GenerateRandomNumber(0, names.Count)];
                int year = Utils.GenerateRandomNumber(_minYear, _maxYear + 1);
                int expirationDate = Utils.GenerateRandomNumber(_minExpirationDate, _maxExpirationDate + 1);

                CannedMeat cannedMeat = new CannedMeat(name, year, expirationDate);

                cannedMeats.Add(cannedMeat);
            }

            return cannedMeats;
        }
    }

    class Utils
    {
        private const int EqualSignCount = 40;
        public static string EqualSignString { get; } = new string('=', EqualSignCount);

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