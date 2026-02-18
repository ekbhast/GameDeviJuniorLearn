using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battlefield battlefield = new Battlefield();
            battlefield.Fight();
        }
    }

    class Battlefield
    {
        private const int RunFight = 1;
        private const int ExitGame = 2;
        
        //public Battlefield(List<Soldier> platoon1, List<Soldier> platoon2)
        //{
        //    Platoon1 = platoon1;
        //    Platoon2 = platoon2;
        //}

        public List<Soldier> Platoon1 = new List<Soldier>();
        public List<Soldier> Platoon2 = new List<Soldier>();

        public void Fight()
        {
            bool isEndGame = false;

            while(isEndGame == false)
            {
                Console.Clear();

                Console.WriteLine($"{RunFight}. Начать битву, в каждом взводе по 10 случайных солдат.");
                Console.WriteLine($"{ExitGame}. Выйти из игры.");

                int command = Utils.ReadInt("Выберете пункт меню");

                switch (command)
                {
                    case RunFight:
                        Console.WriteLine("Битва началась");
                        break;

                    case ExitGame:
                        isEndGame = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню нет");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("До встречи");
            Console.ReadKey();
        }
    }

    public abstract class Soldier
    {
        public Soldier(int health, int damage, int armor)
        {
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public abstract void Attack();
    }

    public class Private : Soldier
    {
        public Private() : base(100, 10 ,5)
        {
        }

        public override void Attack()
        {
        }
    }

    public class Sniper : Soldier
    {
        public Sniper() : base(100, 10, 5)
        {
        }

        public override void Attack()
        {
        }
    }

    public class Gunner : Soldier
    {
        public Gunner() : base(100, 10, 5)
        {
        }

        public override void Attack()
        {
        }
    }

    public class Artillery : Soldier
    {
        public Artillery() : base(100, 10, 5)
        {
        }

        public override void Attack()
        {
        }
    }

    class Utils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int ReadInt(string prompt = "")
        {
            Console.WriteLine(prompt);

            bool isNumber = false;
            int number = 0;

            while (isNumber == false)
            {
                isNumber = int.TryParse(Console.ReadLine(), out number);

                if (isNumber == false)
                {
                    Console.WriteLine("Вы ввели не число.");
                }
            }

            return number;
        }
    }
}
