using System;
using System.Collections.Generic;

namespace Extended_Personnel_accounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddCommand = "1";
            const string ShowAllCommand = "2";
            const string DeleteCommand = "3";
            const string ExitCommand = "4";

            Dictionary<string, List<string>> employees = new Dictionary<string, List<string>>
            {
                ["Программист"] = new List<string>
                {
                    "Иванов Иван Иванович",
                    "Петров Пётр Сергеевич",
                    "Смирнов Алексей Олегович",
                    "Кузнецов Дмитрий Андреевич"
                },
                ["3д художник"] = new List<string>
                {
                    "Сидоров Николай Павлович",
                    "Орлов Михаил Викторович",
                    "Егоров Сергей Владимирович"
                },
                ["Уборщик"] = new List<string>
                {
                    "Климова Анна Игоревна",
                    "Фёдорова Мария Алексеевна",
                    "Васильев Олег Николаевич",
                    "Романова Елена Сергеевна"
                },
                ["Бухгалтер"] = new List<string>
                {
                    "Зайцева Ольга Петровна",
                }
            };

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"{AddCommand}. Добавить сотрудника");
                Console.WriteLine($"{ShowAllCommand}. Показать всех должности и сотрудников");
                Console.WriteLine($"{DeleteCommand}. Удалить сотрудника");
                Console.WriteLine($"{ExitCommand}. Выход");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddCommand:
                        AddEmployee(employees);
                        break;

                    case ShowAllCommand:
                        ShowAllEmployees(employees);
                        break;

                    case DeleteCommand:
                        DeleteFullName(employees);
                        break;

                    case ExitCommand:
                        isExit = true;

                        Console.WriteLine("Выход");
                        break;

                    default:
                        Console.WriteLine("Вы выбрали не существующий пункт меню");
                        break;
                }

                Console.ReadKey();
            }
        }

        private static void AddEmployee(Dictionary<string, List<string>> employees) 
        {
            Console.WriteLine("Введите должность нового сотрудника");
            string position = Console.ReadLine();

            if (employees.ContainsKey(position))
            {
                AddFullName(employees, position);
            }
            else 
            {   
                AddPosition(employees, position);
                AddFullName(employees, position);
            }
        }

        private static void AddFullName(Dictionary<string, List<string>> employees, string position)
        {
            Console.WriteLine("Введите ФИО сотрудника");
            string fullName = Console.ReadLine();

            employees[position].Add(fullName);

            Console.WriteLine($"Сотрудник {fullName}, добавлен.");
        }

        private static void AddPosition(Dictionary<string, List<string>> employees, string position)
        {
            employees.Add(position, new List<string>());
            Console.WriteLine($"Должность {position} не была найдена и была добавлена в базу");
        }

        private static void ShowAllEmployees(Dictionary<string, List<string>> employees)
        {
            foreach (var position in employees.Keys)
            {
                Console.WriteLine($"Должность - {position}\nСотрудники:");

                foreach (var fullName in employees[position]) 
                {
                    Console.WriteLine($"{fullName}");
                }

                Console.WriteLine("\n");
            }
        }

        private static void DeleteFullName(Dictionary<string, List<string>> employees)
        {
            Console.WriteLine("Введите ФИО сотрудника, которого хотите удалить");
            string fullName = Console.ReadLine();

            foreach (var position in employees.Keys) 
            { 
                bool result = employees[position].Remove(fullName);
                if (result == true)
                {
                    Console.WriteLine("Сотрудник удален");
                    DeleteEmptyPosition(employees, position);
                    return;
                }
            }
        }

        private static void DeleteEmptyPosition(Dictionary<string, List<string>> employees, string position)
        {
            if (employees[position].Count == 0)
            {
                employees.Remove(position);
                Console.WriteLine($"Должность {position} не содержит ни одного сотрудника, и поэтому удалена");
            }
        }
    }
}
