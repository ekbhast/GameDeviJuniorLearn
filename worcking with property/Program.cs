using System;

namespace worcking_with_property
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(1, 1, '@');

            Renderer renderer = new Renderer();
            renderer.Render(player);
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Character { get; private set; }

        public Player (int positionX, int positionY, char character)
        {
            PositionX = positionX;
            PositionY = positionY;
            Character = character;
        }
    }

    class Renderer
    {
        public void Render(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Character);
        }
    }
}
