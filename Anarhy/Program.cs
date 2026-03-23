namespace Anarhy
{
    class Program
    {
        static void Main (string[] args)
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

            Hospital hospital = new();
            hospital.Work();            
        }
    }

    class Hospital
    {
        private List<Patient> patients = new();

        private const int SortByFullNameCommand = 1;
        private const int SortByAgeCommand = 2;
        private const int SearchByDiagnosCommand = 3;
        private const int ExitCommand = 4;

        public Hospital()
        {
            
        }

        public void Work()
        {
            bool isExit = false;

            while(isExit == false)
            {
                Console.WriteLine($"{SortByFullNameCommand}. Сортировка по имени");
                Console.WriteLine($"{SortByAgeCommand}. Сортировка по возрасту");
                Console.WriteLine($"{SearchByDiagnosCommand}. Поиск по заболеванию");
                Console.WriteLine($"{ExitCommand}. Выход");

                int command = Utils.ReadInt("Выбирете пункт меню");

                switch (command)
                {
                    case SortByFullNameCommand:
                        break;

                    case SortByAgeCommand:
                        break;

                    case SearchByDiagnosCommand:
                        break;

                    case ExitCommand:
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню не существует");
                        break;
                }

                Console.ReadLine();
            }
        }
    }

    class Patient
    {
        public string FullName { get; private set; }
        public string Diagnosis { get; private set; }
        public int Age { get; private set; }

         public Patient(string fullName, string diagnosis, int age)
        {
            FullName = fullName;
            Diagnosis = diagnosis;
            Age = age;
        }
    }

    class PatientFactory
    {
        public List<Patient> Create(List<string> names, List<string> diagnosis)
        {
            List<Patient> patients = new();

            int patientsCount = 10;
            int minAge = 0;
            int maxAge = 70;

            for (int i = 0; i < patientsCount; i++)
            {
                string name = names[Utils.GenerateRandomNumber(0, names.Count)];
                string diagnos = diagnosis[Utils.GenerateRandomNumber(0, diagnosis.Count)];
                int age = Utils.GenerateRandomNumber(minAge, maxAge + 1);
                Patient patient = new(name, diagnos, age);

                patients.Add(patient);
            }

            return patients;
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
