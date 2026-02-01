using System;

namespace FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string DefaultAttackCommand = "1";
            const string FireballAttackCommand = "2";
            const string ExplosionAttackCommand = "3";
            const string HeroHealthCommand = "4";
            const string ExitGameCommand = "5";

            const string DefaultAttackName = "базовая атака";
            const string FireballAttackName = "атака фаерболом";
            const string ExplosionAttackName = "атака взрывом";
            const string HeroHealingName = "лечение";
            const string ExitGameName = "позорное бегство";

            const string Divider = "======================================";

            int bossHealth = 150;
            int bossAttackDamage;
            int minBossAttackDamage = 10;
            int maxBossAttackDamage = 25;

            string heroName = "";
            int heroHealth = 100;
            int maxHeroHealth = 100;
            int heroMana = 100;
            int maxHeroMana = 100;

            int healingUses = 3;
            int healingAmount = 50;

            int defaultAttackDamage;
            int minDefaultAttackDamage = 10;
            int maxDefalutAttackDamage = 20;

            int fireballAttackDamage;
            int fireballManaPrice = 20;
            int minFireballAttackDamage = 20;
            int maxFireballAttackDamage = 40;

            int explosionAttackDamage;
            int explosionManaPrice = 40;
            int minExplosionAttackDamage = 40;
            int maxExplosionAttackDamage = 80;

            string userChoice = "";
            bool isBossTurn = false;
            bool isEndGame = false;
            bool isFirstScreenInfo = true;
            bool isBurning = false;


            Random random = new Random();

            Console.WindowHeight = 25;
            Console.WindowWidth = 150;

            Console.WriteLine("Введите имя вашего героя");
            heroName = Console.ReadLine();

            while (isEndGame == false)
            {
                Console.Clear();

                if (isFirstScreenInfo)
                {
                    Console.WriteLine($"Приветствуем тебя {heroName} в игре 'Завали босса'.");
                    Console.WriteLine("Твоя цель убить босса.");
                    Console.WriteLine("Игра выполняется в пошаговом режиме.\n");
                    Console.WriteLine($"Тебе доступны четыре действия и позорное бегство:\n" +
                        $"{DefaultAttackCommand}. {DefaultAttackName} наносящая от {minDefaultAttackDamage} до {maxDefalutAttackDamage} урона.\n" +
                        $"{FireballAttackCommand}. {FireballAttackName} наносящая от {minFireballAttackDamage} до {maxFireballAttackDamage} урона и поджигающей врага на 1 ход за {fireballManaPrice} маны.\n" +
                        $"{ExplosionAttackCommand}. {ExplosionAttackName} наносящая от {minExplosionAttackDamage} до {maxExplosionAttackDamage} урона за {explosionManaPrice} маны, " +
                        $"применяется только после {FireballAttackName} в состоянии горит.\n" +
                        $"{HeroHealthCommand}. {HeroHealingName} восстанавливающее на {healingAmount} единиц маны и здоровья.\n" +
                        $"{ExitGameCommand}. {ExitGameName}");
                    Console.Write("Нажми любую клавишу для продолжения");

                    isFirstScreenInfo = false;

                    Console.ReadKey();
                }

                Console.Clear();

                Console.WriteLine($"Босс:\n" +
                    $"Здоровье: {bossHealth}\n");

                if (isBurning)
                {
                    Console.WriteLine("Горит!\n");
                }

                Console.WriteLine(Divider);

                Console.WriteLine($"{heroName}:\n" +
                    $"Здоровье: {heroHealth}\n" +
                    $"Мана: {heroMana}\n" +
                    $"Доступное лечение: {healingUses}");

                Console.WriteLine(Divider);

                if (isBossTurn)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine("Сейчас ходит босс");

                    bossAttackDamage = random.Next(minBossAttackDamage, maxBossAttackDamage + 1);
                    heroHealth -= bossAttackDamage;
                    isBossTurn = !isBossTurn;

                    Console.WriteLine($"Босс наносит вам {bossAttackDamage} урона");
                    Console.ReadKey();

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    continue;
                }

                Console.WriteLine($"Выберите действие:\n" +
                       $"{DefaultAttackCommand}. {DefaultAttackName} наносящая от {minDefaultAttackDamage} до {maxDefalutAttackDamage} урона.\n" +
                       $"{FireballAttackCommand}. {FireballAttackName} наносящая от {minFireballAttackDamage} до {maxFireballAttackDamage} урона и поджигающей врага на 1 ход за {fireballManaPrice} маны.\n" +
                       $"{ExplosionAttackCommand}. {ExplosionAttackName} наносящая от {minExplosionAttackDamage} до {maxExplosionAttackDamage} урона за {explosionManaPrice} маны," +
                       $"применяется только после {FireballAttackName} в состоянии горит.\n" +
                       $"{HeroHealthCommand}. {HeroHealingName} восстанавливающее на {healingAmount} единиц маны и здоровья.\n" +
                       $"{ExitGameCommand}. {ExitGameName}");

                Console.WriteLine(Divider);

                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case DefaultAttackCommand:
                        defaultAttackDamage = random.Next(minDefaultAttackDamage, maxDefalutAttackDamage + 1);
                        bossHealth -= defaultAttackDamage;
                        isBurning = false;

                        Console.WriteLine($"Жмякнули обычной атакой на {defaultAttackDamage}");
                        Console.ReadKey();
                        break;

                    case FireballAttackCommand:
                        if (heroMana < fireballManaPrice)
                        {
                            Console.WriteLine("У вас не хватило маны, переход хода");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            fireballAttackDamage = random.Next(minFireballAttackDamage, maxFireballAttackDamage + 1);
                            bossHealth -= fireballAttackDamage;
                            heroMana -= fireballManaPrice;
                            isBurning = true;

                            Console.WriteLine($"Жмякнули фаерболом на {fireballAttackDamage}");
                            Console.ReadKey();
                        }
                        break;

                    case ExplosionAttackCommand:
                        if (heroMana < explosionManaPrice)
                        {
                            Console.WriteLine("У вас не хватило маны, переход хода");
                            Console.ReadKey();
                            break;
                        }
                        else if (isBurning)
                        {
                            explosionAttackDamage = random.Next(minExplosionAttackDamage, maxExplosionAttackDamage + 1);
                            bossHealth -= explosionAttackDamage;
                            heroMana -= explosionManaPrice;
                            isBurning = false;

                            Console.WriteLine($"Жмякнули взрывом на {explosionAttackDamage}");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Вы пытались взорвать врага, но забыли кинуть в него фаербол. Увы, переход хода");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case HeroHealthCommand:
                        isBurning = false;
                        if (healingUses == 0)
                        {
                            Console.WriteLine("Вам нечем лечиться. Переход хода");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            heroHealth += healingAmount;
                            heroMana += healingAmount;
                            healingUses -= 1;

                            if (heroHealth > maxHeroHealth)
                            {
                                heroHealth = maxHeroHealth;
                            }

                            if (heroMana > maxHeroMana)
                            {
                                heroMana = maxHeroMana;
                            }

                            Console.WriteLine($"Вы восстановили по {healingAmount} едениц здоровья и маны");
                            Console.ReadKey();
                        }
                        break;

                    case ExitGameCommand:
                        Console.WriteLine("Вы позорно сбежали с поля боя.");
                        isEndGame = true;
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Вы пытались что-то изобразить, но у вас ничего не вышло.(Введено несуществующее действие)");
                        Console.WriteLine("И вы пропускаете ход.");
                        Console.ReadKey();
                        break;
                }

                isBossTurn = !isBossTurn;

                if (bossHealth <= 0 || heroHealth <= 0)
                {
                    isEndGame = true;
                }

                Console.Clear();
            }

            if (bossHealth <= 0)
            {
                Console.WriteLine("Вы победили! Игра окончена.");
                Console.ReadKey();
            }
            else if (heroHealth <= 0)
            {
                Console.WriteLine("Увы вы проиграли. Игра окончена.");
                Console.ReadKey();
            }
        }
    }
}