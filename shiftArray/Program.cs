using System;

namespace shiftArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minRandomValue = 0;
            int maxRandomValue = 9;

            int userInput;

            int[] numbers = new int[4];

            Console.WriteLine("Изначальный массив:");

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandomValue, maxRandomValue + 1);
                Console.Write(numbers[i] + " ");
            }

            Console.WriteLine("Сколько раз сдвинуть массив?");
            userInput = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < userInput; i++)
            {
                int firstElement = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = firstElement;
            }

            Console.WriteLine("Новый массив:");

            for (int i = 0; i < numbers.Length; i++) 
            {
                Console.Write(numbers[i] + " ");
            }
        }
    }
}
