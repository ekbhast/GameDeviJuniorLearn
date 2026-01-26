using System;
using System.Collections.Generic;

namespace Extanded_Dynamic_Arrow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string SumCommand = "sum";
            const string ExitCommand = "exit";

            bool isExit = false;

            List<int> numbers = new List<int>();

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"Комманды: \n{SumCommand} - сумма всех введеных чисел \n{ExitCommand} - выход из программы\n");

                Console.WriteLine("Введите число или команду");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SumCommand:
                        Console.WriteLine($"Сумма всех чисел: {GetSum(numbers)}");
                        break;

                    case ExitCommand:
                        Console.WriteLine("Выход");

                        isExit = true;
                        break;

                    default:
                        AddValidNumber(userInput, numbers);
                        break;
                }

                Console.ReadKey();
            }
        }

        private static void AddValidNumber(string userInput, List<int> numbers)
        {
            if (int.TryParse(userInput, out int result))
            {
                numbers.Add(result);
                Console.WriteLine($"Число {result} добавлено \nНажмите любую клавишу для продолжнения...");
            }
            else
            {
                Console.WriteLine("Вы ввели ни число ни команду \nНажмите любую клавишу для продолжнения... ");
            }
        }

        private static int GetSum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            return sum;
        }
    }
}
