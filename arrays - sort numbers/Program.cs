using System;

namespace arrays___sort_numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int maxRandomValue = 10;
            int minRandomValue = 0;

            int[] numbers = new int[10];

            Console.WriteLine("Исходный массив:");

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandomValue, maxRandomValue + 1);
                Console.Write(numbers[i] + " ");
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nОтсортированный массив");

            for (int i = 0; i <= numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }
        }
    }
}
