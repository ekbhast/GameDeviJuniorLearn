using System;

namespace Personnel_accounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddCommand = "1";
            const string ShowAllCommand = "2";
            const string DeleteCommand = "3";
            const string SearchLastnameCommand = "4";
            const string ExitCommand = "5";

            string[] fullNames =
            {
                "Гуров Тимофей Максимович",
                "Скворцова Ульяна Андреевна",
                "Гуров Андрей Кириллович",
                "Максимов Артём Робертович",
                "Герасимов Денис Андреевич"
            };
            string[] positions =
            {
                "Генеральный директор",
                "Исполнительный директор",
                "Технический директор",
                "Финансовый директор",
                "Коммерческий директор"
            };

            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine($"{AddCommand}. Добавить сотрудника");
                Console.WriteLine($"{ShowAllCommand}. Показать всех сотрудников");
                Console.WriteLine($"{DeleteCommand}. Удалить сотрудника");
                Console.WriteLine($"{SearchLastnameCommand}. Найти сотрудника  по фамилии");
                Console.WriteLine($"{ExitCommand}. Выход");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddCommand:
                        AddEmployee(ref fullNames, ref positions);
                        break;

                    case ShowAllCommand:
                        ShowAllEmployees(fullNames, positions);
                        break;

                    case DeleteCommand:
                        DeleteEmployee(ref fullNames, ref positions);
                        break;

                    case SearchLastnameCommand:
                        SearchEmployee(fullNames, positions);
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Выбран не существующий пункт меню");
                        break;
                }
            }
        }

        private static void AddEmployee(ref string[] fullNames, ref string[] positions)
        {
            Console.Clear();
            Console.WriteLine("Введите ФИО нового сутрудника, не менее 3х символов");

            string newFullName = GetValidInput();

            Console.Clear();
            Console.WriteLine("Введите должность сотрудника, не менее 3х символов");

            string newPosition = GetValidInput();

            fullNames = AddArrayItem(newFullName, fullNames);
            positions = AddArrayItem(newPosition, positions);

            Console.WriteLine("Сотрудник успешно добавлен!\n");
            ShowAllEmployees(fullNames, positions);
        }

        private static void DeleteEmployee(ref string[] fullNames, ref string[] positions)
        {
            Console.Clear();

            ShowAllEmployees(fullNames, positions);

            Console.Write("Введите порядковый номер сотрудника которго необходимо удалить: ");

            string inputIndex = Console.ReadLine();
            bool isNumber = int.TryParse(inputIndex, out int employeeNumber);

            if (isNumber && employeeNumber > 0 && employeeNumber <= fullNames.Length)
            {
                Console.WriteLine($"Вы выбрали сотрудника - {fullNames[employeeNumber - 1]}");
                fullNames = RemoveArrayItem(employeeNumber, fullNames);
                positions = RemoveArrayItem(employeeNumber, positions);

                Console.Clear();

                Console.WriteLine($"Все сотрудники:");
                ShowAllEmployees(fullNames, positions);
            }
            else
            {
                Console.WriteLine("Вы ввели не число или порядковый номер которго не существует");
            }

            Console.Write('\n');
        }

        private static void SearchEmployee(string[] fullNames, string[] positions)
        {
            Console.WriteLine("Введите Фамилию сотрудника для поиска");

            string inputLastnameEmployee = Console.ReadLine();
            ShowFilteredEmployees(FilterEmployees(fullNames, inputLastnameEmployee), fullNames, positions);
        }

        private static void ShowFilteredEmployees(int[] filteredIndex, string[] fullNames, string[] positions)
        {
            Console.Clear();

            if (filteredIndex.Length <= 0)
            {
                Console.WriteLine("Не найдено ни одного сотрудника");
            }
            else
            {
                for (int i = 0; i < filteredIndex.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {fullNames[filteredIndex[i]]} - {positions[filteredIndex[i]]}");
                }

                Console.Write('\n');
            }

            Console.Write('\n');
        }

        private static void ShowAllEmployees(string[] fullNames, string[] positions)
        {
            Console.Clear();

            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {fullNames[i]} - {positions[i]}");
            }

            Console.Write('\n');
        }

        private static string[] RemoveArrayItem(int removeIndex, string[] array)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < removeIndex - 1; i++)
            {
                tempArray[i] = array[i];
            }

            for (int i = removeIndex; i < array.Length; i++)
            {
                tempArray[i - 1] = array[i];
            }

            return tempArray;
        }

        private static string[] AddArrayItem(string item, string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            tempArray[tempArray.Length - 1] = item;

            return tempArray;
        }

        private static int[] FilterEmployees(string[] fullNames, string filterValue)
        {
            int[] filteredIndex = new int[0];

            for (int i = 0; i < fullNames.Length; i++)
            {
                string lastname = fullNames[i].Split()[0];

                if (lastname == filterValue)
                {
                    int[] tempFilteredIndex = new int[filteredIndex.Length + 1];

                    for (int j = 0; j < filteredIndex.Length - 1; j++)
                    {
                        tempFilteredIndex[j] = filteredIndex[j];
                    }

                    tempFilteredIndex[tempFilteredIndex.Length - 1] = i;
                    filteredIndex = tempFilteredIndex;
                }
            }

            return filteredIndex;
        }

        private static string GetValidInput()
        {
            string inputString = Console.ReadLine();

            while (inputString.Length <= 3)
            {
                Console.WriteLine("Не менее 3х символов");
                inputString = Console.ReadLine();
            }

            return inputString;
        }
    }
}