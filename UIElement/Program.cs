using System;

namespace UIElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int helath = 70;
            int healthPositionX = 1;
            int healthPositionY = 1;
            int healthBarLength = 33;

            int mana = 68;
            int manaPositionX = 1;
            int manaPositionY = 3;
            int manaBarLength = 10;

            Console.WriteLine("Health:");
            DrawBar(helath, ConsoleColor.Green, healthBarLength, healthPositionX, healthPositionY);

            Console.Write('\n');

            Console.WriteLine("Mana:");
            DrawBar(mana, ConsoleColor.Blue, manaBarLength, manaPositionX, manaPositionY);
        }

        private static void DrawBar(int filledPercent, ConsoleColor fillColor, int barLength, int positionX, int positionY)
        {
            const double BarPercentStep = 10.0;

            int minPercentBarValue = 0;
            int maxPercentBarValue = 100;

            ConsoleColor defaultColor = Console.BackgroundColor;

            if (filledPercent < minPercentBarValue || filledPercent > maxPercentBarValue || barLength <= 0)
            {
                Console.WriteLine("Произошла ошибка. Шкала имеет не допустимые значения");
            }
            else
            {
                double charsPerStep = barLength / BarPercentStep;
                double filledSteps = filledPercent / BarPercentStep * charsPerStep;
                double remainSteps = barLength - filledSteps;

                Console.SetCursorPosition(positionX, positionY);

                Console.Write('[');
                Console.BackgroundColor = fillColor;
                DrawScale((int)Math.Round(filledSteps), ' ');

                Console.BackgroundColor = defaultColor;
                DrawScale((int)Math.Round(remainSteps), '_');
                Console.Write(']');
            }
        }

        private static void DrawScale(int steps, char drawChar)
        {
            for (int i = 0; i < steps; i++)
            {
                Console.Write(drawChar);
            }
        }
    }
}
