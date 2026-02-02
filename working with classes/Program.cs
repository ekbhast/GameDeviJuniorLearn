using System;

namespace working_with_classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Алексей", 36);

            player.ShowInfo();
        }
    }

    class Player
    {
        private string _name;
        private int _age;

        public Player(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя игрока: {_name}, возраст игрока: {_age}");
        }
    }
}
