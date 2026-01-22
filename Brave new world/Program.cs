using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TO DO
//Сделать игровую карту с помощью двумерного массива. Сделать функцию показа карты в консоли.
//Помимо этого, дать пользователю возможность перемещаться по карте и взаимодействовать с элементами
//(например пользователь не может пройти сквозь стену).
//Все элементы являются обычными символами.
//Не используйте Task.Run.

namespace Brave_new_world
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] map = 
            { 
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
            };

            int characterPositionX = 1;
            int characterPositionY = 1;

            char characterView = '@';

            Console.CursorVisible = false;

            while (true)
            {
                ShowMap(map);
                ShowCharacter(characterPositionX, characterPositionY, characterView);

                ConsoleKeyInfo pressedKey = Console.ReadKey();

                MoveCharacter(pressedKey, ref characterPositionX, ref characterPositionY);

                Console.Clear();
            }

            
        }

        private static void ShowMap(char[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }

                Console.Write('\n');
            }
        }

        private static void ShowCharacter(int characterPositionX, int characterPositionY, char characterView)
        {   
            Console.SetCursorPosition(characterPositionX, characterPositionY);
            Console.Write(characterView);
        }

        private static void MoveCharacter(ConsoleKeyInfo pressedKey, ref int characterPositionX, ref int characterPositionY)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    characterPositionY -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    characterPositionY += 1;
                    break;

                case ConsoleKey.LeftArrow:
                    characterPositionX -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    characterPositionX += 1;
                    break;

                default:
                    break;
            }
        }
    }
}
