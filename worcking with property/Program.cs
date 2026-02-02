using System;

namespace worcking_with_property
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(1, 1, '@');

            player.Draw();
        }
    }

    class Player
    {
        private int _positionX;
        private int _positionY;
        private char _displayChar;

        public Player (int positionX, int positionY, char displayChar)
        {
            _positionX = positionX;
            _positionY = positionY;
            _displayChar = displayChar;
        }

        public void Draw()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(_displayChar);
        }
    }
}
