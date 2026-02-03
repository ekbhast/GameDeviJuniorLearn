using System;
using System.Collections.Generic;

namespace players_data_base
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            PlayerCreator creator = new PlayerCreator();
            Menu menu = new Menu();
            Service playerService = new Service(database, creator);

            bool isExit = false;

            while (isExit == false)
            {
                menu.Render();
                int command = menu.GetCommand();

                switch (command)
                {
                    case Menu.AddCommand:
                        playerService.AddPlayer();
                        break;

                    case Menu.BanCommand:
                        playerService.BanPlayer();
                        break;

                    case Menu.UnbanCommand:
                        playerService.UnbanPlayer();
                        break;

                    case Menu.ShowPlayersCommand:
                        database.ShowPlayers();
                        break;

                    case Menu.DeleteCommand:
                        playerService.DeletePlayer();
                        break;

                    case Menu.ExitCommand:
                        Console.WriteLine("Выход");
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавиша для продолжения");
                Console.ReadKey();
            }
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
            int command = GetterNumber.GetNumber("Введите номер команды:");

            return command;
        }
    }

    class Database
    {
        public Dictionary<int, Player> Players { get; private set; } = new Dictionary<int, Player>();

        public void AddPlayer(int id, Player player)
        {
            Players.Add(id, player);
        }

        public void DeletePlayer(int id)
        {
            Players.Remove(id);
        }

        public void ShowPlayers()
        {
            foreach (Player player in Players.Values)
            {
                player.GetInfo();
            }
        }

        public bool TryGetPlayer(int id, out Player player)
        {
            if (Players.ContainsKey(id))
            {
                player = Players[id];
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

        public void GetInfo()
        {
            Console.WriteLine($"ID - {Id}, NickName - {NickName}, Level - {Level}, Ban status - {IsBanned}");
        }
    }

    class PlayerCreator
    {
        public Player Create()
        {
            int id = GetterNumber.GetNumber("Введите уникальный ИД:");

            Console.WriteLine("Введите ник:");
            string nick = Console.ReadLine();

            int level = GetterNumber.GetNumber("Введите уровень:");

            return new Player(id, nick, level);
        }
    }

    class Service
    {
        private Database _database;
        private PlayerCreator _playerCreator;

        public Service(Database database, PlayerCreator playerCreator)
        {
            _database = database;
            _playerCreator = playerCreator;
        }

        public void AddPlayer()
        {
            Player player = _playerCreator.Create();

            if (_database.TryGetPlayer(player.Id, out Player findedPlayer))
            {
                Console.WriteLine("Игрок под таким ID уже существует.");
            }
            else
            {
                _database.AddPlayer(player.Id, player);
                player.GetInfo();
            }
        }

        public void BanPlayer()
        {
            int id = GetterNumber.GetNumber("Введите ID игрока, которого необходимо забанить:");

            if (_database.TryGetPlayer(id, out Player player))
            {
                if (player.IsBanned == true)
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

        public void UnbanPlayer()
        {
            int id = GetterNumber.GetNumber("Введите ID игрока, которого необходимо разбанить");

            if (_database.TryGetPlayer(id, out Player player))
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

        public void DeletePlayer()
        {
            int id = GetterNumber.GetNumber("Введите ID игрока, которого необходимо удалить:");

            if (_database.TryGetPlayer(id, out Player player))
            {
                _database.DeletePlayer(player.Id);
                Console.WriteLine($"Игрок с ID {player.Id} удален из базы");
            }
            else
            {
                Console.WriteLine("Игрока с таким ID не существует.");
            }
        }
    }

    static class GetterNumber
    {
        public static int GetNumber(string prompt)
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