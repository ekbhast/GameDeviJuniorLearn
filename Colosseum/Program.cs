using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Colosseum
{
    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    abstract class Gladiator : IDamageable
    {
        protected Gladiator(int health, int armor, int minDamage, int maxDamage, int minPercent, int maxPercent)
        {
            Health = health;
            Armor = armor;

            MinDamageValue = minDamage;
            MaxDamageValue = maxDamage;

            MinPercent = minPercent;
            MaxPercent = maxPercent;
        }

        public int Health { get; protected set; }
        public int Armor { get; private set; }

        public int MinDamageValue { get; private set; }
        public int MaxDamageValue { get; private set; }

        public int MinPercent { get; private set; }
        public int MaxPercent { get; private set; }

        public abstract string Type { get; }
        public abstract string AbilityName { get; }
        public abstract string AbillityInfo { get; }

        public bool IsAlive
        {
            get { return Health > 0; }
        }

        public int GetDamage()
        {
            int damage = Utils.GenerateRandomNumber(MinDamageValue, MaxDamageValue);
            return damage;
        }

        public virtual void TakeDamage(int damage)
        {
            int damageTaken = damage - Armor;

            if (damageTaken < 0)
            {
                Utils.ChangeTextColor(ConsoleColor.Yellow);
                Console.WriteLine("Но весь урон был поглащен броней.");
                Utils.ChangeTextColor(ConsoleColor.White);

                damageTaken = 0;
            }
            else
            {
                damageTaken = damage - Armor;
            }

            Health -= damageTaken;
        }
    
        public virtual void ShowStats()
        {
            Console.WriteLine($"Класс: {Type}");
            Console.WriteLine($"Здоровье: {Health}");
            Console.WriteLine($"Броня: {Armor}");
            Console.WriteLine($"Максимальный урон: {MaxDamageValue}");
        }

        public virtual void Attack(IDamageable target)
        {
        }

        public void DefaultAttack(int damage, IDamageable target)
        {
            Console.WriteLine($"{Type} нанес {damage} урона обычной атакой");
            target.TakeDamage(damage);
        }

        public abstract Gladiator Clone();
    }

    class DoubleAttack
    {
        private int _attackCount = 0;
        private int _attacksToActivate = 3;

        public string Name { get; } = "Двойная атака";

        public bool TryUse(ref int damage, IDamageable target, IAttacker self)
        {
            _attackCount++;

            if (_attackCount == _attacksToActivate)
            {
                Use(ref damage, target, self);

                _attackCount = 0;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Use(ref int damage, IDamageable target, IAttacker self)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{self.Type} применил умение {Name}");
            Console.ForegroundColor = ConsoleColor.White;

            int extraDamage = self.Attack(target);
        }
    }

    class FuryHealing
    {
        int furyForUseAbility = 20;
        int healingValue = 10;

        public string Name { get; } = "Яростное лечение";

        public bool TryUse(ref int damage, IDamageable target, IAttacker self)
        {
            if (self is Berserker berserker)
            {
                berserker.CollectFury(damage);

                if (berserker.Fury >= furyForUseAbility)
                {
                    Use(ref damage, target, self);
                    berserker.ResetFury();

                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Use(ref int damage, IDamageable target, IAttacker self)
        {
            self.TakeHealilng(healingValue);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{self.Type} применил умение {Name} на {healingValue} лечения");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class FireBall
    {
        int manaCost = 20;

        public string Name { get; } = "Огненный шар";

        public bool TryUse(ref int damage, IDamageable target, IAttacker self)
        {
            if (self is IMagic magicUser && magicUser.CanSpendMana(manaCost))
            {
                Use(ref damage, target, self);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Use(ref int damage, IDamageable target, IAttacker self)
        {
            int extraDamage = 5;
            int fireBallDamage;
            int fireBallMinDamage = damage;
            int fireBallMaxDamage = damage + extraDamage;

            if (self is IMagic magicUser && magicUser.CanSpendMana(manaCost))
            {
                magicUser.SpendMana(manaCost);

                fireBallDamage = Utils.GenerateRandomNumber(fireBallMinDamage, fireBallMaxDamage);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{self.Type} применил умение {Name} на {fireBallDamage} урона");
                Console.ForegroundColor = ConsoleColor.White;

                target.TakeDamage(fireBallDamage);               
            }
        }
    }

    class Dodge
    {
        int _chancePercent = 30;

        public string Name { get; } = "Уклонение";

        public bool TryUse(ref int damage, IDamageable target, IAttacker self)
        {
            int minPercent = 0;
            int maxPercent = 100;

            if (Utils.GenerateRandomNumber(minPercent, maxPercent) <= _chancePercent)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{self.Type} применил умение {Name}");
                Console.ForegroundColor = ConsoleColor.White;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Use(ref int damage, IDamageable target, IAttacker self)
        {
        }
    }

    class Barbarian : Gladiator
    {
        private int _chancePercent = 30;
        private int _damageMultiplicator = 2;

        public Barbarian() : base(100, 3, 0, 10, 0, 100)
        {
        }

        public override string Type { get; } = "Варвар";

        public override string AbilityName { get; } = "Двойной урон";
        public override string AbillityInfo { get; } = "Спец умение -  шанс нанести удвоенный урон";

        public override void Attack(IDamageable target)
        {
            int damage = GetDamage();

            if (Utils.GenerateRandomNumber(MinPercent, MaxPercent) <= _chancePercent)
            {
                damage = damage * _damageMultiplicator;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Type} применил умение \"{AbilityName}\" и удвоил урон до {damage}");
                Console.ForegroundColor = ConsoleColor.White;

                target.TakeDamage(damage);
            }
            else
            {
                DefaultAttack(damage, target);
            }
        }

        public override Gladiator Clone()
        {
            return new Barbarian();
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(AbillityInfo);
        }
    }

    class Juggernaut : Gladiator
    {
        public Juggernaut() : base(100, 3, 0, 10)
        {
        }

        public override string AbilityName { get; } = "Двойной урон";
        public override string AbillityInfo { get; } = "Спец умение - каждую третью свою атаку наносит дважды урон врагу";
        public override string Type { get; } = "Джагернаут";

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(AbillityInfo);
        }

        public override Gladiator Clone()
        {
            return new Juggernaut();
        }
    }

    class Berserker : Gladiator
    {
        public Berserker() : base(100, 3, 0, 10)
        {
        }

        public int Fury { get; private set; } = 0;
        public override string Type { get; } = "Берсерк";
        public override string AbilityName { get; } = "Лечение";
        public override string AbillityInfo { get; } = "Спец умение - получая по себе урон накапливает ярость," +
            " после накопления максимума, использует лечение";

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Ярость - {Fury}");
            Console.WriteLine(AbillityInfo);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public void CollectFury(int damage)
        {
            Fury += damage;
        }
        public void ResetFury()
        {
            Fury = 0;
        }

        public override Gladiator Clone()
        {
            return new Berserker();
        }
    }

    class Pyromancer : Gladiator
    {
        private int _mana = 100;

        public Pyromancer() : base(100, 3, 0, 10)
        {
        }

        public int Mana => _mana;

        public override string Type { get; } = "Пиромансер";
        public override string AbilityName { get; } = "Фаербол";
        public override string AbillityInfo { get; } = "Спец умение - есть мана и пока её достаточно для" +
            " применения заклинания “Огненный шар”, он применяет данное заклинание. " +
            "Заклинание так же наносит урон, но урон больше от изначального";

        public bool CanSpendMana(int manaCost)
        {
            if (Mana >= manaCost)
            {
                return true;
            }
            return false;
        }

        public void SpendMana(int manaCost)
        {
            _mana -= manaCost;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Мана: {_mana}");
            Console.WriteLine(AbillityInfo);
            
        }

        public override Gladiator Clone()
        {
            return new Pyromancer();
        }
    }

    class Assassin : Gladiator
    {
        public Assassin() : base(100, 3, 0, 10)
        {
        }

        public override string Type { get; } = "Ассасин";
        public override string AbilityName { get; } = "Уклонение";
        public override string AbillityInfo { get; } = "Спец умение - шанс уклонится от удара";

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(AbillityInfo);
        }

        public override void TakeDamage(int damage)
        {
        }

        public override Gladiator Clone()
        {
            return new Assassin();
        }
    }

    class Colosseum
    {
        private const int IntroMessageCommand = 1;
        private const int SeeFightCommand = 2;
        private const int ExitCommand = 3;

        private ConsoleColor _default = ConsoleColor.White;
        private ConsoleColor _borderRound = ConsoleColor.Green;
        private ConsoleColor _borderAttack = ConsoleColor.Cyan;

        private List<Gladiator> _gladiators = new List<Gladiator>
        {
            new Barbarian(),
            new Juggernaut(),
            new Berserker(),
            new Pyromancer(),
            new Assassin(),
        };

        private Gladiator _gladiator1;
        private Gladiator _gladiator2;

        public void Work()
        {
            string introMessage = "Приветственное сообщение Колизея!";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                ShowMainMenu();

                int command = Utils.ReadInt("Выберите пункт меню");

                switch (command)
                {
                    case IntroMessageCommand:
                        Console.WriteLine(introMessage);
                        break;

                    case SeeFightCommand:
                        RunFight();
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта нет");
                        break;
                }

                Console.ReadLine();
            }
        }

        public void ShowMainMenu()
        {
            Console.WriteLine($"{IntroMessageCommand}. Приветственное сообщение.");
            Console.WriteLine($"{SeeFightCommand}. Выбрать гладиаторов и начать бой.");
            Console.WriteLine($"{ExitCommand}. Выход.");
        }

        public void ShowListGladiators(List<Gladiator> _gladiators)
        {
            for (int i = 0; i < _gladiators.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_gladiators[i].Type}");
            }
        }

        public void RunFight()
        {
            Gladiator attacker;
            IDamageable damageable;

            ShowGladiatorStats();

            Console.WriteLine("Гладиаторы для выбора:");
            ShowListGladiators(_gladiators);

            Console.WriteLine("Выберете первого гладиатора:");
            _gladiator1 = SelectGladiator();

            Console.WriteLine("Выберете второго гладиатора:");
            _gladiator2 = SelectGladiator();

            while (_gladiator1.IsAlive == true && _gladiator2.IsAlive == true)
            {
                Utils.ChangeTextColor(_borderRound);
                Console.WriteLine(new string('=', 30));
                Utils.ChangeTextColor(_borderAttack);

                Console.WriteLine(new string('-', 30));

                attacker = _gladiator1;
                damageable = _gladiator2;

                Utils.ChangeTextColor(_default);
                attacker.Attack(damageable);

                Utils.ChangeTextColor(_borderAttack);
                Console.WriteLine(new string('-', 30));

                attacker = _gladiator2;
                damageable = _gladiator1;

                Utils.ChangeTextColor(_default);
                attacker.Attack(damageable);

                Utils.ChangeTextColor(_borderAttack);
                Console.WriteLine(new string('-', 30));

                Utils.ChangeTextColor(_default);

                _gladiator1.ShowStats();
                Console.WriteLine();
                _gladiator2.ShowStats();

                Utils.ChangeTextColor(_borderRound);
                Console.WriteLine(new string('=', 30));
                Utils.ChangeTextColor(_default);

                DetermineWinner();
            }
        }

        public void DetermineWinner()
        {
            if (_gladiator1.IsAlive == false && _gladiator2.IsAlive == false)
            {
                Console.WriteLine("Ничья");
            }
            else if (_gladiator1.IsAlive == false)
            {
                Console.WriteLine($"Победил второй гладиатор {_gladiator2.Type}");
            }
            else if (_gladiator2.IsAlive == false)
            {
                Console.WriteLine($"Победил первый гладиатор {_gladiator1.Type}");
            }
        }

        public Gladiator SelectGladiator()
        {
            Gladiator gladiator = null;

            while (gladiator == null)
            {
                int command = Utils.ReadInt();
                gladiator = _gladiators[command - 1].Clone();
            }

            return gladiator;
        }

        public void ShowGladiatorStats()
        {
            foreach (Gladiator gladiator in _gladiators)
            {
                gladiator.ShowStats();
                Console.WriteLine(new string('-', 30));
            }
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

        public static void ChangeTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Colosseum colosseum = new Colosseum();
            colosseum.Work();
        }
    }
}