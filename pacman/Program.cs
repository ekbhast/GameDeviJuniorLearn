using System;
using System.IO;
using System.Threading;

namespace pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char [,] map = ReadMap("map.txt");

            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            int pacmanX = 1;
            int pacmanY = 1;

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(31, 0);
                Console.Write(pressedKey.KeyChar);

                pressedKey = Console.ReadKey();

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map);
            }
        }

        private static char [,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[file[0].Length, file.Length];

            for (int line = 0; line < map.GetLength(0); line++)
            {
                for(int column = 0; column < map.GetLength(1); column++)
                {
                    map[line, column] = file[column][line];
                }
            }

            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int line = 0; line < map.GetLength(1); line++)
            {
                for (int column = 0; column < map.GetLength(0); column++)
                {
                    Console.Write(map[column, line]);
                }

                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map)
        {
            int [] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            if (map[nextPacmanPositionX, nextPacmanPositionY] == ' ')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;
            }
        }

        private static int [] GetDirection (ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;

            }

            return direction;
        }
    }
}
