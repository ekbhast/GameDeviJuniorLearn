using System;
using System.Collections.Generic;

namespace players_data_base
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            PlayerCreator creator = new PlayerCreator();
            Menu menu = new Menu();
            Service playerService = new Service(dataBase, creator);

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
                        dataBase.ShowPlayers();
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

    class DataBase
    {
        public Dictionary<int, Player> Players = new Dictionary<int, Player>();

        public void AddPlayer(int id, Player player)
        {
            Players.Add(id, player);
        }

        public bool IsUniqueId(Player player)
        {
            return Players.ContainsKey(player.Id) == false;
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
        private DataBase _dataBase;
        private PlayerCreator _playerCreator;

        public Service(DataBase dataBase, PlayerCreator playerCreator)
        {
            _dataBase = dataBase;
            _playerCreator = playerCreator;
        }

        public void AddPlayer()
        {
            Player player = _playerCreator.Create();

            if (_dataBase.IsUniqueId(player))
            {
                _dataBase.AddPlayer(player.Id, player);
                player.GetInfo();
            }
            else
            {
                Console.WriteLine("Игрок под таким ID уже существует.");
            }
        }

        public void BanPlayer()
        {
            int id = GetterNumber.GetNumber("Введите ID игрока, которого необходимо забанить:");

            if (_dataBase.Players.ContainsKey(id))
            {
                if (_dataBase.Players[id].IsBanned == true)
                {
                    Console.WriteLine("Игрок уже забанен");
                }
                else
                {
                    _dataBase.Players[id].Ban();
                    Console.WriteLine($"Игрок c ID {_dataBase.Players[id].Id} - забанен.");
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

            if (_dataBase.Players.ContainsKey(id))
            {
                if (_dataBase.Players[id].IsBanned == false)
                {
                    Console.WriteLine("Игрок уже разбанен");
                }
                else
                {
                    _dataBase.Players[id].Unban();
                    Console.WriteLine($"Игрок c ID {_dataBase.Players[id].Id} - разбанить.");
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

            if (_dataBase.Players.ContainsKey(id))
            {
                _dataBase.DeletePlayer(id);
                Console.WriteLine($"Игрок с ID {id} удален из базы");
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
            while (true)
            {
                Console.WriteLine(prompt);
                string inputUser = Console.ReadLine();

                if (int.TryParse(inputUser, out int number))
                    return number;

                Console.WriteLine("Введено не число!\n");
            }
        }
    }
}