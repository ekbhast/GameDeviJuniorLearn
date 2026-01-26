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
            const string SearchLastnameCommand = "4";
            const string ExitCommand = "5";

            Dictionary<string, string> fullNames = new Dictionary<string, string>();

            List<string> positions = new List<string> 
            {
                "Программист",
                "Директор",
                "Уборщик",
                "Бухгалтер",
            };

            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine($"{AddCommand}. Добавить сотрудника");
                Console.WriteLine($"{ShowAllCommand}. Показать всех сотрудников");
                Console.WriteLine($"{DeleteCommand}. Удалить сотрудника");
                Console.WriteLine($"{SearchLastnameCommand}. Найти сотрудника  по фамилии");
                Console.WriteLine($"{ExitCommand}. Выход");

                Console.ReadKey();
            }
        }
    }
}
