using System;
using System.Collections.Generic;

namespace players_data_base
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            PlayerFactory creator = new PlayerFactory();
            Menu menu = new Menu();

            bool isExit = false;

            while (isExit == false)
            {
                menu.Render();
                int command = menu.GetCommand();

                switch (command)
                {
                    case Menu.AddCommand:
                        database.AddPlayer(creator);
                        break;

                    case Menu.BanCommand:
                        database.BanPlayer();
                        break;

                    case Menu.UnbanCommand:
                        database.UnbanPlayer();
                        break;

                    case Menu.ShowPlayersCommand:
                        database.ShowPlayers();
                        break;

                    case Menu.DeleteCommand:
                        database.DeletePlayer();
                        break;

                    case Menu.ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }

            Console.WriteLine("Выход");
        }
    }

    class Menu
    {
        public const int AddCommand = 1;
        public const int BanCommand = 2;
        public const int UnbanCommand = 3;
        public const int ShowPlayersCommand = 4;
        public const int DeleteCommand = 5;
        public const int ExitCommand = 6;

        public void Render()
        {
            Console.Clear();

            Console.WriteLine($"{AddCommand}. Добавить игрока.");
            Console.WriteLine($"{BanCommand}. Забанить игрока.");
            Console.WriteLine($"{UnbanCommand}. Разбанить игрока.");
            Console.WriteLine($"{ShowPlayersCommand}. Показать всех игроков.");
            Console.WriteLine($"{DeleteCommand}. Удалить игрока.");
            Console.WriteLine($"{ExitCommand}. Выход.");
        }

        public int GetCommand()
        {
            int command = UserUtils.ReadInt("Введите номер команды:");

            return command;
        }
    }

    class Database
    {
        private Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _lastId = 0;

        public void AddPlayer(PlayerFactory playerFactory)
        {
            Player player = playerFactory.Create(_lastId);
            _players.Add(player.Id, player);
            _lastId++;

            player.ShowInfo();
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player.Id);
                Console.WriteLine($"Игрок с ID {player.Id} удален из базы");
            }
            else
            {
                Console.WriteLine("Игрока с таким ID не существует.");
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                if (player.IsBanned == false)
                {
                    Console.WriteLine("Игрок уже разбанен");
                }
                else
                {
                    player.Unban();
                    Console.WriteLine($"Игрок c ID {player.Id} - разбанeн.");
                }
            }
            else
            {
                Console.WriteLine("Игрока с таким ID не существует.");
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                if (player.IsBanned)
                {
                    Console.WriteLine("Игрок уже забанен");
                }
                else
                {
                    player.Ban();
                    Console.WriteLine($"Игрок c ID {player.Id} - забанен.");
                }
            }
            else
            {
                Console.WriteLine("Игрока с таким ID не существует.");
            }
        }

        public void ShowPlayers()
        {
            if (_players.Count == 0)
            {
                Console.WriteLine("Игроков в базе нет");
            }
            else
            {
                foreach (Player player in _players.Values)
                {
                    player.ShowInfo();
                }
            }
        }

        public bool TryGetPlayer(out Player player)
        {
            int id = UserUtils.ReadInt("Введите ID игрока:");

            if (_players.ContainsKey(id))
            {
                player = _players[id];
                return true;
            }
            else
            {
                player = null;
                return false;
            }
        }
    }

    class Player
    {
        public Player(int id, string nickName, int level, bool isBanned = false)
        {
            Id = id;
            NickName = nickName;
            Level = level;
            IsBanned = isBanned;
        }

        public int Id { get; private set; }
        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID - {Id}, NickName - {NickName}, Level - {Level}, Ban status - {IsBanned}");
        }
    }

    class PlayerFactory
    {
        public Player Create(int lastId)
        {
            int id = lastId;

            Console.WriteLine("Введите ник:");
            string nick = Console.ReadLine();

            int level = UserUtils.ReadInt("Введите уровень:");

            return new Player(id, nick, level);
        }
    }

    static class UserUtils
    {
        public static int ReadInt(string prompt)
        {
            int number = 0;
            bool isNumber = false;

            while (isNumber == false)
            {
                Console.WriteLine(prompt);
                string inputUser = Console.ReadLine();

                if (int.TryParse(inputUser, out number))
                    return number;
            }

            return number;
        }
    }
}