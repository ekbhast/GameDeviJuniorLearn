using System;
using System.Collections.Generic;
using System.Linq;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Animal>> animalsDictionary = new Dictionary<string, List<Animal>>()
            {
                ["Хищные птицы"] = new List<Animal>()
                {
                    new Animal("Орёл", "Самец", "Криии"),
                    new Animal("Орёл", "Самка", "Криии"),
                    new Animal("Ястреб", "Самец", "Киии"),
                    new Animal("Ястреб", "Самка", "Киии"),
                    new Animal("Сова", "Самец", "Ух-ух"),
                    new Animal("Сова", "Самка", "Ух-ух"),
                    new Animal("Сокол", "Самец", "Кууу"),
                    new Animal("Сокол", "Самка", "Кууу"),
                    new Animal("Филин", "Самец", "Уу-ух"),
                    new Animal("Филин", "Самка", "Уу-ух")
                },

                ["Хищные кошки"] = new List<Animal>()
                {
                    new Animal("Лев", "Самец", "Рррр"),
                    new Animal("Лев", "Самка", "Рррр"),
                    new Animal("Тигр", "Самец", "Рааа"),
                    new Animal("Тигр", "Самка", "Рааа"),
                    new Animal("Пантера", "Самец", "Шшш"),
                    new Animal("Пантера", "Самка", "Шшш"),
                    new Animal("Ягуар", "Самец", "Гррр"),
                    new Animal("Ягуар", "Самка", "Гррр"),
                    new Animal("Гепард", "Самец", "Ррр"),
                    new Animal("Гепард", "Самка", "Ррр")
                },

                ["Медведи"] = new List<Animal>()
                {
                    new Animal("Бурый медведь", "Самец", "Уррр"),
                    new Animal("Бурый медведь", "Самка", "Уррр"),
                    new Animal("Белый медведь", "Самец", "Рааа"),
                    new Animal("Белый медведь", "Самка", "Рааа"),
                    new Animal("Гризли", "Самец", "Гррр"),
                    new Animal("Гризли", "Самка", "Гррр"),
                    new Animal("Панда", "Самец", "Ммм"),
                    new Animal("Панда", "Самка", "Ммм"),
                    new Animal("Камчатский медведь", "Самец", "Рррр"),
                    new Animal("Камчатский медведь", "Самка", "Рррр")
                },

                ["Обезьяны"] = new List<Animal>()
                {
                    new Animal("Шимпанзе", "Самец", "Уу-уу"),
                    new Animal("Шимпанзе", "Самка", "Уу-уу"),
                    new Animal("Горилла", "Самец", "Аа-а"),
                    new Animal("Горилла", "Самка", "Аа-а"),
                    new Animal("Орангутанг", "Самец", "Хо-хо"),
                    new Animal("Орангутанг", "Самка", "Хо-хо"),
                    new Animal("Бабуин", "Самец", "Урр"),
                    new Animal("Бабуин", "Самка", "Урр"),
                    new Animal("Мартышка", "Самец", "Ии-и"),
                    new Animal("Мартышка", "Самка", "Ии-и")
                }
            };

            EnclosureFactory enclosureFactory = new EnclosureFactory();
            Zoo zoo = new Zoo(animalsDictionary, enclosureFactory);
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Enclosure> _enclosures = new List<Enclosure>();
        private int enclosuresCount = 10;

        public Zoo(Dictionary<string, List<Animal>> animalsDictionary, EnclosureFactory enclosureFactory)
        {
            _enclosures = enclosureFactory.Create(animalsDictionary, enclosuresCount);
        }

        public void Work()
        {
            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine("0. Выйти");
                ShowEnclosures();

                int number = Utils.ReadInt("Выберите номер вольера к которому подойти:");

                if (number == 0)
                {
                    isExit = true;
                    continue;
                }

                if (number <= 0 || number > _enclosures.Count)
                {
                    Console.WriteLine("Такого вальера не существует");
                    Console.ReadKey();
                }
                else
                {
                    _enclosures[number - 1].Show();
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Выход");
        }

        public void ShowEnclosures()
        {
            for (int i = 0; i < _enclosures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_enclosures[i].Species}");
            }
        }
    }

    class Enclosure
    {
        private List<Animal> _animals = new List<Animal>();

        public Enclosure(string species, List<Animal> animals)
        {
            Species = species;
            _animals = new List<Animal>(animals);
        }

        public string Species { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Вольер: {Species}");
            Console.WriteLine($"Количество животных:  {_animals.Count}");

            foreach (Animal animal in _animals)
            {
                Console.WriteLine($"Вид:{animal.Name}, Пол: {animal.Sex}, Звук: {animal.Sound}");
            }
        }
    }

    class EnclosureFactory
    {
        private int minAnimalsCount = 1;
        private int maxAnimalsCount = 5;

        public List<Enclosure> Create(Dictionary<string, List <Animal>> animalsDictionary, int enclosureCount)
        {
            List<Enclosure> enclosures = new List<Enclosure>();

            for (int i = 0; i < enclosureCount; i++)
            {
                int animalsCount = Utils.GenerateRandomNumber(minAnimalsCount, maxAnimalsCount + 1);

                List<string> keys = animalsDictionary.Keys.ToList();
                string randomSpecies = keys[Utils.GenerateRandomNumber(0, keys.Count)];

                List<Animal> enclosureAnimals = new List<Animal>();
                List<Animal> animalsDictionaryList = animalsDictionary[randomSpecies];

                for (int j = 0; j < animalsCount; j++)
                {
                    enclosureAnimals.Add(animalsDictionaryList[Utils.GenerateRandomNumber(0, animalsDictionaryList.Count)].Clone());
                }

                enclosures.Add(new Enclosure(randomSpecies, enclosureAnimals));
            }

            return enclosures;
        }
    }

    class Animal
    {
        public Animal(string name, string sex, string sound)
        {
            Name = name;
            Sex = sex;
            Sound = sound;
        }

        public string Name { get; private set; }
        public string Sex { get; private set; }
        public string Sound { get; private set; }

        public Animal Clone()
        {
            return new Animal(Name, Sex, Sound);
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
