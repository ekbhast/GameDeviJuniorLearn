using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minRandomValue = 0;
            int maxRandomValue = 100;

            int purchaseCount = 10;

            int account = 0;

            Queue<int> amounts = new Queue<int>();

            for (int i = 0; i < purchaseCount; i++)
            {
                amounts.Enqueue(random.Next(minRandomValue, maxRandomValue + 1));
            }

            while (amounts.Count > 0)
            {
                Console.Clear();

                Console.WriteLine($"Ваш счет: {account} деняк\n");

                Console.WriteLine("Вам принесли денежку:");
                Console.WriteLine($"{amounts.Peek()}");               

                Console.WriteLine("Нажмите любую клавишу, чтобы принять оплату и пополнить счет");

                Console.ReadKey();
                account += amounts.Dequeue();
            }

            Console.Clear();
            Console.WriteLine("Очередь закончилась");
        }
    }
}
