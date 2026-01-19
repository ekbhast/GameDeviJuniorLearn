using System;

namespace Shuffle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[5];
            int minRandomValue = 0;
            int maxRandomValue = 100;

            Random random = new Random();

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandomValue, maxRandomValue + 1);
            }

            Console.WriteLine("Исходный массив:");
            DrawArray(numbers);

            Shuffle(numbers, random);

            Console.Write('\n');

            Console.WriteLine("Измененнный массив:");
            DrawArray(numbers);
        }

        private static void Shuffle(int[] array, Random random)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                int temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        private static void DrawArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
        }
    }
}
