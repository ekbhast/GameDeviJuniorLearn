using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shuffle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[5];
            int minRandomValue = 0;
            int maxRandomValue = 100;

            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomValue, maxRandomValue + 1);
            }

            Console.WriteLine("Исходный массив:");
            DrawArray(array);

            Shuffle(ref array, random);

            Console.Write('\n');

            Console.WriteLine("Измененнный массив:");
            DrawArray(array);
        }

        private static void Shuffle(ref int[] arr, Random random)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int randomItem = random.Next(i + 1);
                int temp = arr[i];
                arr[i] = arr[randomItem];
                arr[randomItem] = temp;
            }
        }

        private static void DrawArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
