using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Supermarket
    {
        private Queue<Customer> _customers = new Queue<Customer>();
        private Dictionary<string, int> _products = new Dictionary<string, int>();
        private int _balance = 0;

        private const int ServeCustomer = 1;
        private const int GetNewQueueCommand = 2;
        private const int Exit = 3;

        public void Work()
        {
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine($"{ServeCustomer}. Обслужить следующего покупателя");
                Console.WriteLine($"{GetNewQueueCommand}. Получить новых покупателей");
                Console.WriteLine($"{Exit}. Выход");

                int command = Utils.ReadInt("Выберете пункт меню:");

                switch (command)
                {
                    case ServeCustomer:
                        break;

                    case GetNewQueueCommand:
                        break;

                    case Exit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Вы выбрали несуществующий пункт меню.");
                        break;
                }
            }

            Console.WriteLine("Выход.");
            Console.ReadKey();
        }
    }

    class Customer
    {
        private int _balance;
        private Dictionary <string, int> _cart = new Dictionary<string, int>();
        private Dictionary <string, int> _bag = new Dictionary<string, int>();


    }

    class Utils
    {
        private static readonly Random s_random = new Random();

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

        public static void ChangeTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            supermarket.Work();
        }
    }
}
