namespace Munhunt
{
    class Program
    {
        static void Main(string[] args)
        {
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
            private const int Exit = 2;

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
                    ShowAllCriminals();

                    int command = Utils.ReadInt("Выбирете пункт меню");

                    switch (command)
                    {
                        case StartSearchCommand: 
                            Console.WriteLine("Сработал поиск");
                            break;

                        case Exit:
                            isExit = true;
                            break;

                        default: 
                            Console.WriteLine("Вы выбрали не существующий пункт меню");
                            break;
                    }

                    Console.ReadKey();
                }
            }

            public void ShowAllCriminals()
            {
                foreach(Criminal criminal in _criminals)
                {
                    Console.WriteLine($"{criminal.FullName} | {criminal.Nationality} | {criminal.IsInPrison} | {criminal.Height} | {criminal.Weight}");
                }
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

            public List<Criminal> Create(List<string> names, List<string> nationality)
            {
                List<Criminal> criminals = new List<Criminal>();

                for(int i = 0; i < _criminalsCount; i++)
                {
                    var name = names[Utils.GenerateRandomNumber(0, names.Count)];
                    var isInPrison = Utils.GetRandomBoolean();
                    var height = Utils.GenerateRandomNumber(_minHeight, _maxHeight + 1);
                    var weight = Utils.GenerateRandomNumber(_minWeight, _maxWeight + 1);
                    var nat = nationality[Utils.GenerateRandomNumber(0, nationality.Count)];

                    criminals.Add(new Criminal(name, isInPrison, height, weight, nat));
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