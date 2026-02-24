using System;
using System.Collections.Generic;

namespace Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    class Aquarium
    {
        private List<Fish> _fish = new List<Fish>();
        private int _maxFishCount = 10;

        private const int Add = 1;
        private const int Remove = 2;
        private const int NextDay = 3;
        private const int ExitProgram = 4;

        public int FishCount => _fish.Count;

        public void Work()
        {
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                Console.WriteLine($"{Add}. Добавить рыбу в аквариум");
                Console.WriteLine($"{Remove}. Достать рыбу из аквариума");
                Console.WriteLine($"{NextDay}. Закончить день");
                Console.WriteLine($"{ExitProgram}. Выйти из программы");

                ViewFish();

                int command = Utils.ReadInt("\nВыберете пункт меню");

                switch (command)
                {
                    case Add:
                        TryAddFish();
                        break;

                    case Remove:
                        TryRemoveFish();
                        break;

                    case NextDay:
                        FinishDay();
                        break;

                    case ExitProgram:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню не существует.");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход");
        }

        public void TryAddFish()
        {
            if (FishCount >= _maxFishCount) 
            {
                Console.WriteLine("Аквариум полный!");
            }
            else{
                Fish fish = new Fish();
                _fish.Add(fish);
            }
            
        }

        public void TryRemoveFish()
        {
            if (FishCount == 0)
            {
                Console.WriteLine("Аквариум пуст!");
            }
            else 
            {
                int number = Utils.ReadInt("Введите номер рыбы, которую нужно достать:");

                if (number < 1 || number > FishCount)
                {
                    Console.WriteLine("Вы ввели некорректный номер.");
                }
                else
                {
                    _fish.RemoveAt(number - 1);
                    Console.WriteLine("Вы достали рыбу из аквариума.");
                }
            }
        }

        public void ViewFish()
        {
            Console.WriteLine("Аквариум:");

            if (FishCount == 0)
            {
                Console.WriteLine("Аквариум пуст");
            }
            else
            {
                for (int i = 0; i < FishCount; i++)
                {
                    Console.WriteLine($"{i + 1}. Рыба прожила {_fish[i].Age} из {_fish[i].MaxAge} дней, и ей осталось {_fish[i].DaysToDeath}");
                }
            }
        }

        public void FinishDay()
        {
            for (int i = FishCount - 1; i >= 0; i--)
            {
                _fish[i].IncreaseAge();

                if (_fish[i].IsAlive == false)
                {
                    _fish.RemoveAt(i);
                }
            }

            Console.WriteLine("Прошел день");
        }
    }

    class Fish
    {
        private int _age = 0;
        private int _maxAge;
        private int _minLifeTime = 1;
        private int _maxLifeTime = 10;

        public Fish()
        {
            _maxAge = Utils.GenerateRandomNumber(_minLifeTime, _maxLifeTime + 1);
        }

        public bool IsAlive => DaysToDeath > 0;
        public int MaxAge => _maxAge;
        public int Age => _age;
        public int DaysToDeath => _maxAge - _age;

        public void IncreaseAge()
        {
            if (IsAlive)
            {
                _age++;
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
    }
}
