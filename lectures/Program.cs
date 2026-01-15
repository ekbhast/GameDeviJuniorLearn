using System;

namespace lectures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DrawBar(5, 10, 1, ConsoleColor.Green);
            DrawBar(6, 10, 2, ConsoleColor.Blue);
        }

        static void DrawBar (int value, int maxValue, int position, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            
            Console.SetCursorPosition(0, position);

            Console.Write('[');

            Console.BackgroundColor = color;

            for (int i = 0; i <= value; i++) 
            {
                Console.Write(' ');
            }

            Console.BackgroundColor = defaultColor;

            for (int i = value; i <= maxValue; i++) { 
                Console.Write(" ");
            }

            Console.WriteLine(']');
        }
    }
}
