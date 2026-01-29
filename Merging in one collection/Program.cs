using System;
using System.Collections.Generic;

namespace Merging_in_one_collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] stringsOne =
            {
                "дом",
                "река",
                "стол",
                "кот",
                "дом",
                "книга",
                "облако",
                "машина",
                "книга",
                "солнце",
                "лес",
                "окно"
            };

            string[] stringsTwo =
            {
                "окно",
                "птица",
                "кот",
                "город",
                "река",
                "телефон",
                "лес",
                "чашка",
                "компьютер",
                "стул"
            };

            List<string> resultStrings = new List<string>();

            AddUniqueString(stringsOne, resultStrings);
            AddUniqueString(stringsTwo, resultStrings);

            Console.WriteLine("Объединенная коллекция:");

            foreach(var element in resultStrings)
            {
                Console.WriteLine(element);
            }
        }

        private static void AddUniqueString(string[] array, List<string> resultStrings)
        {
            foreach(var element in array)
            {
                if (resultStrings.Contains(element) == false) 
                {
                resultStrings.Add(element);
                }
            }
        }
    }
}
