using System;

namespace ReadInt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = ReadInt();
            Console.WriteLine($"Вы ввели корректное число: {number}");
        }

        private static int ReadInt()
        {
            bool isNumber = false;
            int number = 0;

            while (isNumber == false)
            {
                Console.Clear();

                Console.WriteLine("Введите число: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    return number;
                }

                Console.WriteLine("Введено не число");
                Console.ReadKey();
            }

            return number;
        }
    }
}