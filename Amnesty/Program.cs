namespace Amnesty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            List<string>  names = new ()
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

            List<string> crimes = new()
            {
                "Кража",
                "Ограбление",
                "Кража со взломом",
                "Мошенничество",
                "Антиправительственное"
            };

            AmnestyProgram amnestyProgram = new(names, crimes);
            amnestyProgram.Start();
        }

        class AmnestyProgram
        {
            private const int GrantAmnestyCommand = 1;
            private const int ExitCommand = 2;

            private List<Prisoner> _prisoners = new ();

            public AmnestyProgram(List<string> names, List<string> crimes)
            {
                PrisonerFactorey prisonerFactorey = new();
                _prisoners = prisonerFactorey.Create(names, crimes);
            }

            public void Start()
            {
                Console.Clear();

                bool isExit = false;

                ShowPrisoner();

                while(isExit == false)
                {
                    Console.WriteLine($"{GrantAmnestyCommand}. Объявить амнистию.");
                    Console.WriteLine($"{ExitCommand}. Выход");

                    int command = Utils.ReadInt("Обявите амнистию, или завершите программу");

                    switch (command)
                    {
                        case GrantAmnestyCommand:
                            GrantAmnesty();
                            break;
                        
                        case ExitCommand:
                            isExit = true;
                            break;

                        default:
                            Console.WriteLine("Такого пункта меню нет.");
                            break;
                    }

                    Console.ReadKey();
                }
            }

            public void ShowPrisoner()
            {
                foreach(var prisoner in _prisoners)
                {
                    Console.WriteLine($"Имя заключенного - {prisoner.FullName}\nПреступление - {prisoner.Crime}");
                    Console.WriteLine(new string('=', 30));
                }
            }

            public void GrantAmnesty()
            {
                string crimeForAmnesty = "Антиправительственное";
                
                _prisoners = _prisoners.Where(prisoner => prisoner.Crime != crimeForAmnesty).ToList();

                Console.WriteLine("\nСписок заключенных после амнистии");
                ShowPrisoner();
            }
        }

        class Prisoner
        {
            public string FullName {get; private set;}
            public string Crime {get; private set;}

            public Prisoner (string fullName, string crime)
            {
                FullName = fullName;
                Crime = crime;
            }
        }

        class PrisonerFactorey
        {
            private int _prisonerCount = 10;

            public List<Prisoner> Create(List<string> names, List<string> crimes)
            {
                List<Prisoner> prisoners = new List<Prisoner>();

                for (int i = 0; i < _prisonerCount; i++)
                {
                    Prisoner prisoner = new (names[Utils.GenerateRandomNumber(0, names.Count)], crimes[Utils.GenerateRandomNumber(0, crimes.Count)]);
                    prisoners.Add(prisoner);
                }

                return prisoners;
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