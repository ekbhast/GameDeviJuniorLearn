using System;

namespace FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minRandomValue = 0;
            int maxRandomValue = 9;

            int maxRepeatNumber;
            int maxCountRepeat = 1;
            int currentCount = 1;

            int[] numbers = new int[30];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandomValue, maxRandomValue + 1);
                Console.Write(numbers[i] + " ");
            }

            maxRepeatNumber = numbers[0];

            Console.WriteLine();

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    currentCount++;
                    if ( currentCount >= maxCountRepeat)
                    {
                        maxRepeatNumber = numbers[i];
                        maxCountRepeat = currentCount;
                    }
                }
                else
                {
                    currentCount = 1;
                }
            }

            if(maxCountRepeat == 1)
            {
                Console.WriteLine("Повторяющихся чисел не найдено");
            }
            else
            {
                Console.WriteLine($"Число {maxRepeatNumber} повторяется {maxCountRepeat} раза подряд");
            }
        }
    }
}