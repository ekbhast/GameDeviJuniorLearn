using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extanded_Dynamic_Arrow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string SumCommand = "sum";
            const string ExitCommand = "exit";

            bool exit = false;

            List<int> numbers = new List<int>();

            while (exit == false)
            {
                Console.Clear();
                Console.WriteLine($"Комманды: \n{SumCommand} - сумма всех введеных чисел \n{ExitCommand} - выход из программы\n");
                Console.WriteLine("Введите число или команду");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SumCommand:
                        Console.WriteLine("Вы ввели команду sum");
                        Console.ReadKey();

                        break;

                    case ExitCommand:
                        Console.WriteLine("Вы ввели команду exit");
                        Console.ReadKey();
                        break;

                    default:
                        if (int.TryParse(userInput, out int result))
                        {
                            numbers.Add(result);
                            Console.WriteLine($"Число {result} добавлено \nНажмите любую клавишу для продолжнения...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели ни число ни команду \nНажмите любую клавишу для продолжнения... ");
                            Console.ReadKey();
                        }
                            break;
                }
            }
        }
    }
}
