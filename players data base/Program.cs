using System;
using System.Collections.Generic;

namespace players_data_base
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int AddCommand = 1;
            const int BanCommand = 2;
            const int UnbanCommand = 3;
            const int ShowPlayersCommand = 4;
            const int DeleteCommand = 5;
            const int ExitCommand = 6;

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"{AddCommand}. Добавить игрока.");
                Console.WriteLine($"{BanCommand}. Забанить игрока.");
                Console.WriteLine($"{UnbanCommand}. Разбанить игрока.");
                Console.WriteLine($"{DeleteCommand}. Удалить игрока.");
                Console.WriteLine($"{ExitCommand}. Выход.");

                DataBase dataBase = new DataBase();

                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int command))
                {
                    switch (command)
                    {
                        case AddCommand:
                            dataBase.AddPlayer();
                            break;

                        case BanCommand:
                            Console.WriteLine(BanCommand);
                            break;

                        case UnbanCommand:
                            Console.WriteLine(UnbanCommand);
                            break;

                        case ShowPlayersCommand:
                            dataBase.ShowPlayers();
                            break;

                        case DeleteCommand:
                            Console.WriteLine(DeleteCommand);
                            break;

                        case ExitCommand:
                            isExit = true;
                            break;

                        default:
                            Console.WriteLine("Такой команды не существует.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели не число.");
                }

                Console.ReadKey();
            }
        }

        class DataBase
        {
            public List<Player> Players { get; private set; }
            public void AddPlayer(Player player)
            {
                Players.Add(player);
            }

            public void BanPlayer()
            {

            }

            public void UnbanPlayer()
            {

            }

            public void DeletePlayer()
            {

            }

            public void ShowPlayers()
            {
                foreach (Player p in Players)
                {
                    Console.WriteLine($"ID - {p.Id}, NickName - {p.NickName}, Level - {p.Level}, Ban status - {p.IsBanned}");
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
        }
    }
}