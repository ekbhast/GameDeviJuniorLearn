using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace _1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>()
            {
                new Player("Джон", 100),
                new Player("Билл", 210),
                new Player("Дерек", 3110),
                new Player("Klark", 2340)
            };

            var newPlayers = from Player player in players select new { Name = player.Name, dateOfBirth = DateTime.Now };
            var newPlayers2 = players.Select(player => new { Name = player.Name, dateOfBirth = DateTime.Now });
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }

        public Player(string login, int level)
        {
            Name = login;
            Level = level;
        }
    }
}
