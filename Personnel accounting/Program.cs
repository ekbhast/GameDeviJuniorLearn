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

            const string AddItemMenuName = "Добавить сотрудника";
            const string ShowAllItemMenuName = "Показать всех сотрудников";
            const string DeleteItemMenuName = "Удалить сотрудника";
            const string SearchLastnameItemMenuName = "Найти сотрудника  по фамилии";
            const string ExitItemMenuName = "Выход";

            string[] employees = 
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

            while(isExit == false)
            {
                Console.WriteLine($"{AddCommand}. {AddItemMenuName}");
                Console.WriteLine($"{ShowAllCommand}. {ShowAllItemMenuName}");
                Console.WriteLine($"{DeleteCommand}. {DeleteItemMenuName}");
                Console.WriteLine($"{SearchLastnameCommand}. {SearchLastnameItemMenuName}");
                Console.WriteLine($"{ExitCommand}. {ExitItemMenuName}");

                string userInput = Console.ReadLine();

                switch (userInput) 
                {
                    case AddCommand:
                        AddEmployee(ref employees, ref positions);
                        break;

                    case ShowAllCommand:
                        ShowEmployees(FilterEmployees(employees), employees, positions);
                        break;

                    case DeleteCommand:
                        DeleteEmployee(ref employees, ref positions);
                        break;

                    case SearchLastnameCommand:
                        SearchEmployee(ref employees, ref positions);
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

        private static void AddEmployee(ref string[] employees, ref string[] positions)
        {
            Console.Clear();
            Console.WriteLine("Введите ФИО нового сутрудника, не менее 3х символов");

            string newEmployee = CheckImputLength();

            Console.Clear();
            Console.WriteLine("Введите должность сотрудника, не менее 3х символов");

            string position = CheckImputLength();
            CreateEmployee(newEmployee, position, ref employees, ref positions);

            Console.WriteLine("Сотрудник успешно добавлен!\n");
            ShowEmployees(FilterEmployees(employees), employees, positions);
        }

        private static void DeleteEmployee(ref string[] employees, ref string[] positions)
        {
            Console.Clear();

            ShowEmployees(FilterEmployees(employees), employees, positions);

            Console.Write("Введите порядковый номер сотрудника которго необходимо удалить: ");

            string inputIndex = Console.ReadLine();

            int removeIndex;
            bool isNumber = int.TryParse(inputIndex, out removeIndex);

            if (isNumber && removeIndex > 0 && removeIndex <= employees.Length)
            {
                Console.WriteLine($"Вы выбрали сотрудника - {employees[removeIndex - 1]}");
                DeleteEmployee(removeIndex, ref employees, ref positions);

                Console.Clear();

                Console.WriteLine($"Все сотрудники:");
                ShowEmployees(FilterEmployees(employees), employees, positions);
            }
            else
            {
                Console.WriteLine("Вы ввели не число или порядковый номер которго не существует");
            }

            Console.Write('\n');
        }

        private static void SearchEmployee(ref string[] employees, ref string[] positions) 
        {
            Console.WriteLine("Введите Фамилию сотрудника для поиска");

            string inputLastnameEmployee = Console.ReadLine();
            ShowEmployees(FilterEmployees(employees, inputLastnameEmployee), employees, positions);
        }

        private static void CreateEmployee(string newEmployee, string position, ref string[] employees, ref string[] positions) 
        {
            AddArrayItem(newEmployee, ref employees);
            AddArrayItem(position, ref positions);
            Console.Clear();
        }

        private static void ShowEmployees(int[] filteredIndex, string[] employees, string[] positions)
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
                    Console.WriteLine($"{i + 1} - {employees[filteredIndex[i]]} - {positions[filteredIndex[i]]}");
                }
                Console.Write('\n');
            }

            Console.Write('\n');

        }

        private static void DeleteEmployee(int indexEmployee,  ref string [] employees, ref string[] positions) 
        {
            RemoveArrayItem(indexEmployee, ref employees);
            RemoveArrayItem(indexEmployee, ref positions);
        }

        private static void RemoveArrayItem(int removeIndex, ref string[] array)
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

            array = tempArray;
        }

        private static void AddArrayItem(string item, ref string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            tempArray[tempArray.Length - 1] = item;

            array = tempArray;
        }

        private static int[] FilterEmployees(string[] employees, string filterLastname = "all")
        {
            int[] filteredIndex = new int[0];

            if (filterLastname == "all")
            {
                filteredIndex = new int[employees.Length];

                for (int i = 0; i < filteredIndex.Length; i++)
                {
                    filteredIndex[i] = i;
                }

                return filteredIndex;
            } 
            else
            {
                for (int i = 0; i <= employees.Length - 1 ; i++) 
                {
                    string lastname = employees[i].Split()[0];

                    if (lastname == filterLastname)
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
        }

        private static string CheckImputLength()
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
