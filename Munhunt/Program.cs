namespace Munhunt
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

            var nationalities = new List<string>
            {
                "Русский",
                "Украинец",
                "Белорус",
                "Казах",
                "Узбек",
                "Татарин",
                "Армянин",
                "Грузин",
                "Азербайджанец",
                "Немец",
                "Француз",
                "Итальянец",
                "Испанец",
                "Американец",
                "Китаец"
            };

            SearchProgram searchProgram = new SearchProgram(names, nationalities);
            searchProgram.Start();
        }

        class SearchProgram
        {
            private const int StartSearchCommand = 1;
            private const int ShowAllCriminalsCommand = 2;
            private const int ExitCommand = 3;

            private List<Criminal> _criminals = new List<Criminal>();

            public SearchProgram(List<string> names, List<string> nationalities)
            {
                CriminalFactory criminalFactory = new CriminalFactory();
                _criminals = criminalFactory.Create(names, nationalities);
            }            

            public void Start()
            {
                bool isExit = false;

                while(isExit == false)
                {
                    Console.Clear();

                    Console.WriteLine($"{StartSearchCommand}. Найти преступников");
                    Console.WriteLine($"{ShowAllCriminalsCommand}. Показать вех преступников");
                    Console.WriteLine($"{ExitCommand}. Выход");

                    int command = Utils.ReadInt("Выбирете пункт меню");

                    switch (command)
                    {
                        case StartSearchCommand: 
                            SearchCriminals();
                            break;

                        case ShowAllCriminalsCommand:
                            ShowCriminals(_criminals);
                            break;

                        case ExitCommand:
                            isExit = true;
                            break;

                        default: 
                            Console.WriteLine("Вы выбрали не существующий пункт меню");
                            break;
                    }

                    Console.ReadKey();
                }
            }

            public void ShowCriminals(List<Criminal> criminals)
            {
                if (criminals.Count == 0)
                {
                    Console.WriteLine("Нет преступников для показа");
                }
                else
                {
                     foreach(Criminal criminal in criminals)
                    {
                        Console.WriteLine($"ФИО - {criminal.FullName}\n"+
                            $"Национальность - {criminal.Nationality}\n"+
                            $"Под стражей - {criminal.IsInPrison}\n"+
                            $"Рост - {criminal.Height}\n"+
                            $"Вес - {criminal.Weight}");

                        Console.WriteLine(new string('=', 30));
                    }
                }
            }

            public void SearchCriminals()
            {
                int height = Utils.ReadInt("Введите рост подозреваемого: ");
                int weight = Utils.ReadInt("Введите вес подозреваемого: ");

                Console.WriteLine("Введите национальность подозреваемого: ");
                string nationality = Console.ReadLine();

                List<Criminal> criminals = _criminals
                    .Where(criminal => 
                        criminal.IsInPrison == false &&
                        criminal.Height == height &&
                        criminal.Weight == weight &&
                        criminal.Nationality.Equals(nationality, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Console.WriteLine(new string('+', 30));
                Console.WriteLine("Найденные преступники");
                ShowCriminals(criminals);
            }
        }

        class Criminal
        {
            public Criminal(string fullName, bool isInPrison, int height, int weight, string nationality)
            {
                FullName = fullName;
                IsInPrison = isInPrison;
                Height = height;
                Weight = weight;
                Nationality = nationality;
            }

            public string FullName { get; private set; }
            public bool IsInPrison { get; private set; }     
            public int Height { get; private set; }
            public int Weight { get; private set; }
            public string Nationality {get; private set;}
        }

        class CriminalFactory
        {
            private int _criminalsCount = 15;
            private int _maxHeight = 210;
            private int _minHeight = 160;
            private int _maxWeight = 200;
            private int _minWeight = 60;

            public List<Criminal> Create(List<string> names, List<string> nationalities)
            {
                List<Criminal> criminals = new List<Criminal>();

                for(int i = 0; i < _criminalsCount; i++)
                {
                    string name = names[Utils.GenerateRandomNumber(0, names.Count)];
                    bool isInPrison = Utils.GetRandomBoolean();
                    int height = Utils.GenerateRandomNumber(_minHeight, _maxHeight + 1);
                    int weight = Utils.GenerateRandomNumber(_minWeight, _maxWeight + 1);
                    string nationality = nationalities[Utils.GenerateRandomNumber(0, nationalities.Count)];

                    criminals.Add(new Criminal(name, isInPrison, height, weight, nationality));
                }

                return criminals;
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
    }
}