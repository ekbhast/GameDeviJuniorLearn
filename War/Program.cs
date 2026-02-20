using System;
using System.Collections.Generic;

namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlatoonFactory platoonFactory = new PlatoonFactory();
        
            Battlefield battlefield = new Battlefield(platoonFactory);
            battlefield.StartBattle();
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    interface ISquad
    {
        int GetRandomIndexSoldier();
        IDamageable GetSoldier(int index);
        int Count { get; }
    }

    class Battlefield
    {
        private const int RunFight = 1;
        private const int ExitGame = 2;

        private Platoon _platoon1;
        private Platoon _platoon2;
        private PlatoonFactory _platoonFactory;

        public Battlefield(PlatoonFactory platoonFactory)
        {
            _platoonFactory = platoonFactory;
        }

        public void StartBattle()
        {
            bool isEndGame = false;

            while (isEndGame == false)
            {
                Console.Clear();

                Console.WriteLine($"{RunFight}. Начать битву, в каждом взводе по 10 случайных солдат.");
                Console.WriteLine($"{ExitGame}. Выйти из игры.");

                int command = Utils.ReadInt("Выберете пункт меню");

                switch (command)
                {
                    case RunFight:
                        StartFight();
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

        public void StartFight()
        {
            _platoon1 = _platoonFactory.Create();
            _platoon2 = _platoonFactory.Create();

            Console.WriteLine($"Первый взвод перед боем:");
            _platoon1.ShowInfo();

            Console.WriteLine();

            Console.WriteLine($"Второй взвод перед боем:");
            _platoon2.ShowInfo();

            while (_platoon1.IsAlive && _platoon2.IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Атака первого взвода!");
                Console.ForegroundColor = ConsoleColor.White;

                _platoon1.Attack(_platoon2);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Атака второго взвода!");
                Console.ForegroundColor = ConsoleColor.White;

                _platoon2.Attack(_platoon1);

                _platoon1.RemoveDeadSoldiers();
                _platoon2.RemoveDeadSoldiers();

                Console.WriteLine($"Взвод 1:");
                _platoon1.ShowInfo();

                Console.WriteLine($"Взвод 2:");
                _platoon2.ShowInfo();
            }

            ShowWinner();
        }

        public void ShowWinner()
        {
            if (_platoon1.IsAlive == false && _platoon2.IsAlive == false)
            {
                Console.WriteLine("Ничья");
            }
            else if (_platoon1.IsAlive == false)
            {
                Console.WriteLine($"Победил  взвод под номером 2");
            }
            else if (_platoon2.IsAlive == false)
            {
                Console.WriteLine($"Победил  взвод под номером 1");
            }
        }
    }

    abstract class Soldier : IDamageable
    {
        public Soldier(int health, int damage, int minDamage, int armor, string type)
        {
            Health = health;
            MaxDamage = damage;
            MinDamage = minDamage;
            Armor = armor;
            Type = type;
        }

        public int Health { get; private set; }
        public int Armor { get; private set; }
        public string Type { get; private set; }
        public bool IsAlive => Health > 0;
        public int MaxDamage { get; private set; } = 10;
        public int MinDamage { get; private set; } = 0;

        public abstract void Attack(ISquad platoon);

        public abstract Soldier Clone();

        public int GetDamage()
        {
            int damage = Utils.GenerateRandomNumber(MaxDamage, MinDamage);
            return damage;
        }

        public virtual void TakeDamage(int damage)
        {
            int damageTaken = damage - Armor;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            Health -= damageTaken;

            if (Health < 0)
            {
                Health = 0;
            }
        }
    }

    class Platoon :ISquad
    {
        private List<Soldier> _soldiers;

        public Platoon(List<Soldier> soldiers)
        {
            _soldiers = soldiers;
        }

        public int Count => _soldiers.Count;

        public void ShowInfo()
        {
            Console.WriteLine($"В взводе живых еще: {_soldiers.Count} ");
            foreach (Soldier soldier in _soldiers)
            {
                Console.WriteLine($"Боец {soldier.Type} - Здоровье: {soldier.Health}");
            }
        }

        public void RemoveDeadSoldiers()
        {
            _soldiers.RemoveAll(soldier => soldier.IsAlive == false);
        }

        public bool IsAlive => _soldiers.Count > 0;
        
        public void Attack(ISquad enemys)
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.Attack(enemys);
            }
        }

        public int GetRandomIndexSoldier()
        {
            int minIndex = 0;
            int index = Utils.GenerateRandomNumber(minIndex, _soldiers.Count);
            return index;
        }

        public IDamageable GetSoldier(int index)
        {
            return _soldiers[index];
        }
    }

    class PlatoonFactory
    {
        private int _platoonCount = 10;
        private int _minIndex = 0;

        private List<Soldier> _soldiers = new List<Soldier>
        {
            new Private(),
            new Sniper(),
            new Gunner(),
            new Grenadier()
        };

        public Platoon Create()
        {
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < _platoonCount; i++)
            {
                soldiers.Add(_soldiers[Utils.GenerateRandomNumber(_minIndex, _soldiers.Count)].Clone());
            }

            Platoon platoon = new Platoon(soldiers);

            return platoon;
        }
    }

    class Private : Soldier
    {
        public Private() : base(100, 0, 10, 5, "Рядовой")
        {
        }

        public override void Attack(ISquad enemys)
        {
            int damage = GetDamage();

            int randomIndex = enemys.GetRandomIndexSoldier();
            IDamageable target = enemys.GetSoldier(randomIndex);

            target.TakeDamage(damage);
        }

        public override Soldier Clone()
        {
            return new Private();
        }
    }

    class Sniper : Soldier
    {
        private int _damageMultiplicator = 2;

        public Sniper() : base(100, 0, 10, 5, "Снайпер")
        {
        }

        public override void Attack(ISquad enemys)
        {
            int damage = GetDamage() * _damageMultiplicator;

            int randomIndex = enemys.GetRandomIndexSoldier();
            IDamageable target = enemys.GetSoldier(randomIndex);

            target.TakeDamage(damage);
        }

        public override Soldier Clone()
        {
            return new Sniper();
        }
    }

    class Gunner : Soldier
    {
        private int _targetCount = 3;

        public Gunner() : base(100, 0, 10, 5, "Пулеметчик")
        {
        }

        public override void Attack(ISquad enemys)
        {
            int attacks = _targetCount;
            if (attacks > enemys.Count)
            {
                attacks = enemys.Count;
            }

            List<int> attackedTargetsIndexes = new List<int>();

            for (int i = 0; i < attacks; i++)
            {
                int damage = GetDamage();

                int randomIndex = enemys.GetRandomIndexSoldier();

                while (attackedTargetsIndexes.Contains(randomIndex))
                {
                    randomIndex = enemys.GetRandomIndexSoldier();
                }

                IDamageable target = enemys.GetSoldier(randomIndex);

                target.TakeDamage(damage);
                attackedTargetsIndexes.Add(randomIndex);
            }
        }

        public override Soldier Clone()
        {
            return new Gunner();
        }
    }

    class Grenadier : Soldier
    {
        private int _targetCount = 3;

        public Grenadier() : base(100, 0, 10, 5, "Гренадер")
        {
        }

        public override void Attack(ISquad enemys)
        {
            for (int i = 0; i < _targetCount; i++)
            {
                int damage = GetDamage();
                int randomIndex = enemys.GetRandomIndexSoldier();
                IDamageable target = enemys.GetSoldier(randomIndex);

                target.TakeDamage(damage);
            }
        }

        public override Soldier Clone()
        {
            return new Grenadier();
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
