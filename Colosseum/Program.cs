using System;
using System.Collections.Generic;

namespace Colosseum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Colosseum colosseum = new Colosseum();
            colosseum.Work();
        }
    }
    class Colosseum
    {
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
            MainMenu mainMenu = new MainMenu();
            GladiatorsMenu chooseGladiatorMenu = new GladiatorsMenu();

            string introMessage = "Приветственное сообщение Колизея!";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                mainMenu.ShowMainMenu();

                int command = Utils.ReadInt("Выберите пункт меню");

                switch (command)
                {
                    case MainMenu.IntroMessageCommand:
                        Console.WriteLine(introMessage);
                        break;

                    case MainMenu.SeeFightCommand:
                        RunFight(chooseGladiatorMenu);
                        break;

                    case MainMenu.ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта нет");
                        break;
                }

                Console.ReadLine();
            }
        }

        public void RunFight(GladiatorsMenu gladiatorMenu)
        {
            Console.Clear();

            ShowGladiatorStats();

            Console.WriteLine("Гладиаторы для выбора:");
            gladiatorMenu.Show();

            Console.WriteLine("Выберете первого гладиатора:");
            _gladiator1 = SelectGladiator(_gladiators);

            Console.WriteLine("Выберете второго гладиатора:");
            _gladiator2 = SelectGladiator(_gladiators);

            int damage;

            while (_gladiator1.IsAlive == true && _gladiator2.IsAlive == true)
            {
                Console.WriteLine(new string('=', 30));

                damage = _gladiator2.Attack();
                _gladiator1.TakeDamage(damage);

                damage = _gladiator1.Attack();
                _gladiator2.TakeDamage(damage);

                Console.WriteLine($"Здоровье первого гладиатора {_gladiator1.Type} : {_gladiator1.Health}");
                Console.WriteLine($"Здоровье второго гладиатора {_gladiator2.Type}: {_gladiator2.Health}");
            }

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

        public Gladiator SelectGladiator(List<Gladiator> gladiators)
        {
            Gladiator gladiator = null;

            while (gladiator == null)
            {
                int command = Utils.ReadInt();

                switch (command)
                {
                    case GladiatorsMenu.BarbarianCommand:
                        gladiator = gladiators[GladiatorsMenu.BarbarianCommand - 1].Clone();
                        break;

                    case GladiatorsMenu.JuggernautCommand:
                        gladiator = gladiators[GladiatorsMenu.JuggernautCommand - 1].Clone();
                        break;

                    case GladiatorsMenu.BerserkerCommand:
                        gladiator = gladiators[GladiatorsMenu.BerserkerCommand - 1].Clone();
                        break;

                    case GladiatorsMenu.PyromancerCommand:
                        gladiator = gladiators[GladiatorsMenu.PyromancerCommand - 1].Clone();
                        break;

                    case GladiatorsMenu.AssassinCommand:
                        gladiator = gladiators[GladiatorsMenu.AssassinCommand - 1].Clone();
                        break;

                    default:
                        Console.WriteLine("Такого гладиатора нет");
                        break;
                }
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

    abstract class Gladiator
    {
        public abstract string Type { get; }
        public abstract string SpecialAbilityName { get; }
        public abstract string SpecialAbilityInfo { get; }
        public int Health { get; protected set; } = 100;
        public int Armor { get; private set; } = 100;
        public int MinDamageValue { get; private set; } = 1;
        public int MaxDamageValue { get; private set; } = 10;
        public int MinPercentValue { get; private set; } = 0;
        public int MaxPercentValue { get; private set; } = 100;

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
            Health -= damage;
        }

        public virtual void ShowStats()
        {
            Console.WriteLine($"Класс - {Type}");
            Console.WriteLine($"Здоровье - {Health}");
            Console.WriteLine($"Броня - {Armor}");
            Console.WriteLine($"Максимальный урон - {MaxDamageValue}");
        }

        public void ShowInfoDamage(int damage)
        {
            Console.WriteLine($"{Type} выбивает {damage} урона");
            Console.WriteLine("---");
        }
        
        public void ShowAbilityUsed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Type} применил {SpecialAbilityName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public abstract int Attack();
        public abstract Gladiator Clone();
    }

    class Barbarian : Gladiator
    {
        private int _chancePercent = 30;

        public override string SpecialAbilityName { get; } = "Двойной урон";
        public override string SpecialAbilityInfo { get; } = "Спец умение -  шанс двойного урона";
        public override string Type { get; } = "Варвар";

        public override int Attack()
        {
            int damage = GetDamage();
            bool isCritical = Utils.GenerateRandomNumber(MinPercentValue, MaxPercentValue) < _chancePercent;

            if (isCritical)
            {
                ShowAbilityUsed();
                damage += damage;
            }

            ShowInfoDamage(damage);
            return damage;
        }

        public override Gladiator Clone()
        {
            return new Barbarian();
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(SpecialAbilityInfo);
        }
    }

    class Juggernaut : Gladiator
    {
        private int _attackCounter = 0;

        public override string SpecialAbilityName { get; } = "Двойной урон";
        public override string SpecialAbilityInfo { get; } = "Спец умение - каждую третью свою атаку наносит дважды урон врагу";
        public override string Type { get; } = "Джагернаут";

        public override int Attack()
        {
            int damage = GetDamage();
            _attackCounter++;
            if (_attackCounter == 3)
            {
                ShowAbilityUsed();
                damage += damage;
                _attackCounter = 0;
            }

            ShowInfoDamage(damage);
            return damage;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(SpecialAbilityInfo);
        }

        public override Gladiator Clone()
        {
            return new Juggernaut();
        }
    }

    class Berserker : Gladiator
    {
        private int _fury = 0;
        private int _maxStackFury = 4;
        private int _healingValue = 10;

        public override string Type { get; } = "Берсерк";
        public override string SpecialAbilityName { get; } = "Лечение";
        public override string SpecialAbilityInfo { get; } = "Спец умение - получая по себе урон накапливает ярость, после накопления максимума, использует лечение";

        public override int Attack() 
        {
            int damage = GetDamage();
            _fury++;

            if ( _fury == _maxStackFury)
            {
                ShowAbilityUsed();
                Health += _healingValue;
                _fury = 0;
            }

            ShowInfoDamage(damage);
            return damage;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(SpecialAbilityInfo);
        }

        public override Gladiator Clone()
        {
            return new Berserker();
        }
    }

    class Pyromancer : Gladiator
    {
        private int _mana = 100;
        private int _manaCost = 25;
        private int _fireBallDamage = 4;

        public override string Type { get; } = "Пиромансер";
        public override string SpecialAbilityName { get; } = "Фаербол";
        public override string SpecialAbilityInfo { get; } = "Спец умение - есть мана и пока её достаточно для" +
            " применения заклинания “Огненный шар”, он применяет данное заклинание. " +
            "Заклинание так же наносит урон, но урон больше от изначального";

        public override int Attack()
        {
            int damage = GetDamage();

            _mana -= _manaCost;

            if ( _mana >= _manaCost)
            {
                ShowAbilityUsed();
                damage += _fireBallDamage;
            }

            ShowInfoDamage(damage);
            return damage;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(SpecialAbilityInfo);
        }

        public override Gladiator Clone()
        {
            return new Pyromancer();
        }
    }

    class Assassin : Gladiator
    {
        private bool _hasDodged = false;
        private int _chancePercent = 30;

        public override string Type { get; } = "Ассасин";
        public override string SpecialAbilityName { get; } = "Уклонение";
        public override string SpecialAbilityInfo { get; } = "Спец умение - шанс уклонится от удара";

        public override int Attack()
        {
            int damage = GetDamage();

            _hasDodged = Utils.GenerateRandomNumber(MinPercentValue, MaxPercentValue) < _chancePercent;

            if (_hasDodged) 
            {
                ShowAbilityUsed();
            }

            ShowInfoDamage(damage);
            return damage; 
        }

        public override void TakeDamage(int damage)
        {
            if (_hasDodged) 
            {
                base.TakeDamage(0);
            }
            else
            {
                base.TakeDamage(damage);

            }
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine(SpecialAbilityInfo);
        }

        public override Gladiator Clone()
        {
            return new Assassin();
        }
    }

class MainMenu
    {
        public const int IntroMessageCommand = 1;
        public const int SeeFightCommand = 2;
        public const int ExitCommand = 3;

        public void ShowMainMenu()
        {
            Console.WriteLine($"{IntroMessageCommand}. Приветственное сообщение.");
            Console.WriteLine($"{SeeFightCommand}. Выбрать гладиаторов и начать бой.");
            Console.WriteLine($"{ExitCommand}. Выход.");
        }
    }

    class GladiatorsMenu
    {
        public const int BarbarianCommand = 1;
        public const int JuggernautCommand = 2;
        public const int BerserkerCommand = 3;
        public const int PyromancerCommand = 4;
        public const int AssassinCommand = 5;

        public void Show()
        {
            Console.WriteLine($"{BarbarianCommand}. Варвар.");
            Console.WriteLine($"{JuggernautCommand}. Джагернаут.");
            Console.WriteLine($"{BerserkerCommand}. Берсерк");
            Console.WriteLine($"{PyromancerCommand}. Пиромансер");
            Console.WriteLine($"{AssassinCommand}. Ассасин");
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
            Console.Write(prompt);

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