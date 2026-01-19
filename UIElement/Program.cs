using System;

namespace UIElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Health:");
            Console.Write('[');
            DrawBar(60, ConsoleColor.Green, 10, 1, 1);
            Console.Write(']');

            Console.Write('\n');

            Console.WriteLine("Mana:");
            Console.Write('[');
            DrawBar(64, ConsoleColor.Blue, 10, 1, 3);
            Console.Write(']');
        }

        private static void DrawBar(int filledPercent, ConsoleColor fillColor, int barLength, int positionX, int positionY)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;

            const int BarPercentStep = 10;

            int sharsPerStep = barLength / BarPercentStep;
            int filledSteps = filledPercent / BarPercentStep * sharsPerStep;

            Console.SetCursorPosition(positionX, positionY);

            for (int i = 0; i < barLength; i++)
            {
                if (i < filledSteps)
                {
                    Console.BackgroundColor = fillColor;
                    Console.Write(' ');
                }
                else
                {
                    Console.BackgroundColor = defaultColor;
                    Console.Write('_');
                }
            }

            Console.BackgroundColor = defaultColor;
        }
    }
}
