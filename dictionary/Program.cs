using System;
using System.Collections.Generic;

namespace dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                ["Class"] = "Шаблон для создания объектов, описывает их свойства и поведение",
                ["Object"] = "Экземпляр класса, представляющий конкретную сущность",
                ["Method"] = "Блок кода, выполняющий определённое действие",
                ["Variable"] = "Именованная область памяти для хранения данных",
                ["Interface"] = "Контракт, определяющий набор методов без реализации",
                ["Inheritance"] = "Механизм наследования свойств и методов другого класса",
                ["Encapsulation"] = "Сокрытие внутренней реализации и защита данных",
                ["Polymorphism"] = "Возможность работать с разными типами через общий интерфейс",
                ["Struct"] = "Значимый тип данных для хранения небольших наборов данных",
                ["Enum"] = "Набор именованных констант"
            };

            Console.WriteLine("Добро пожаловать в справочник по языку программирования C#");

            Console.WriteLine("Введите термин из языка C# и получите краткую справку.");
            string userInput = Console.ReadLine();

            if (dictionary.ContainsKey(userInput))
            {
                Console.WriteLine($"{userInput} - {dictionary[userInput]}");
            }
            else
            {
                Console.WriteLine("К сожалению такого термина, пока что в базе нет.");
            }
        }
    }
}
