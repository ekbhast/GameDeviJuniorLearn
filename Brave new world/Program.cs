using System;

namespace Brave_new_world
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] map = 
            { 
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', '#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', '#', ' ', '#', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', '#', '#', '#', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
            };
           
            char characterView = '@';

            int characterPositionX = 1;
            int characterPositionY = 1;

            bool isInfinityLoop = true;
            
            Console.CursorVisible = false;

            while (isInfinityLoop)
            {
                ShowMap(map);
                ShowCharacter(characterPositionX, characterPositionY, characterView);

                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                
                MovePlayer(GetDirection(pressedKey.Key),ref characterPositionX,  ref characterPositionY, map);

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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(characterView);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        private static int [] GetDirection(ConsoleKey pressedKey)
        {
            const ConsoleKey UpCommand = ConsoleKey.UpArrow;
            const ConsoleKey DownCommand = ConsoleKey.DownArrow;
            const ConsoleKey LeftCommand = ConsoleKey.LeftArrow;
            const ConsoleKey RightCommand = ConsoleKey.RightArrow;

            int[] direction = { 0, 0 };
                        
            switch (pressedKey)
            {
                case UpCommand:
                    direction[1] = -1;
                    break;

                case DownCommand:
                    direction[1] = +1;
                    break;

                case LeftCommand:
                    direction[0] = -1;
                    break;

                case RightCommand:
                    direction[0] = +1;
                    break;
            }

            return direction;
        }

        private static void MovePlayer(int[] direction, ref int characterPositionX, ref int characterPositionY, char[,] map)
        {            
            int nextCharacterPositionX = characterPositionX + direction[0];
            int nextcharacterPositionY = characterPositionY + direction[1];

            char nextCell = map[nextcharacterPositionY, nextCharacterPositionX];
    
            if (nextCell == ' ')
            {
                characterPositionX = nextCharacterPositionX;
                characterPositionY = nextcharacterPositionY;
            }
        }
    }
}
