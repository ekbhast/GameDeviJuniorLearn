using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.Work();
        }
    }

    class Library
    {
        private List<Book> _books;

        private string _searchParamAll = "all";
        private string _searchParamName = "name";
        private string _searchParamAuthor = "author";
        private string _searchParamYear = "year";


        public Library()
        {
            _books = new List<Book>();
        }

        public void Work()
        {
            List<string> books = new List<string>
            {
                "Война и мир, Лев Толстой, 1869",
                "Преступление и наказание, Фёдор Достоевский, 1866",
                "Идиот, Фёдор Достоевский, 1869",
                "Анна Каренина, Лев Толстой, 1877",
                "Мастер и Маргарита, Михаил Булгаков, 1967",
                "Евгений Онегин, Александр Пушкин, 1833",
                "Обломов, Иван Гончаров, 1859",
                "Братья Карамазовы, Фёдор Достоевский, 1880",
                "Герой нашего времени, Михаил Лермонтов, 1840"
            };

            Menu menu = new Menu();
            BookFactory bookFactory = new BookFactory();
            _books = new List<Book>(bookFactory.Create(books));
            Utils utils = new Utils();

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                menu.Show();

                int command = utils.ReadInt("\nВыберите пункт меню");

                switch (command)
                {
                    case Menu.AddCommand:
                        break;
                    case Menu.DeleteCommand:
                        break;
                    case Menu.SearchNameCommand:
                        Show(Search(_searchParamName));
                        break;
                    case Menu.SearchAuthorCommand:
                        Show(Search(_searchParamAuthor));
                        break;
                    case Menu.SearchYearCommand:
                        Show(Search(_searchParamYear));
                        break;
                    case Menu.ShowAllCommand:
                        Show(Search(_searchParamAll));
                        break;
                    case Menu.ExitCommand:
                        isExit = true;
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Выход");
            Console.ReadKey();
        }

        public void Add()
        {

        }

        public void Delete()
        {

        }

        public void Show(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Ни чего не найдено");
            }
            else
            {
                foreach (Book book in books)
                {
                    Console.WriteLine($"{book.Author}, {book.Name}, {book.Year}");
                }
            }
        }

        public List<Book> Search(string param)
        {
            if (param == "all")
                return _books;

            Utils utils = new Utils();
            List<Book> filteredBooks = new List<Book>();

            int userInputNumber = 0;
            string userInput = "";

            if (param == _searchParamYear)
            {
                userInputNumber = utils.ReadInt("Введите год");
            }
            else
            {
                Console.WriteLine("Что ищем?");
                userInput = Console.ReadLine();
            }                

            foreach (Book book in _books)
            {
                if (param == _searchParamName && book.Name == userInput)
                {
                    filteredBooks.Add(book);
                }
                else if (param == _searchParamAuthor && book.Author == userInput)
                {
                    filteredBooks.Add(book);
                }
                else if (param == _searchParamYear && book.Year == userInputNumber)
                {
                    filteredBooks.Add(book);
                }
            }

            return filteredBooks;
        }
    }

    class Book
    {
        public Book(string name, string author, int year)
        {
            Name = name;
            Author = author;
            Year = year;
        }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }

    }

    class BookFactory
    {
        public List<Book> Create(List<string> data)
        {
            List<Book> books = new List<Book>();

            foreach (string item in data)
            {
                string[] parts = item.Split(',');

                string name = parts[0].Trim();
                string author = parts[1].Trim();
                int year = Convert.ToInt32(parts[2].Trim());

                books.Add(new Book(name, author, year));
            }

            return books;
        }
    }

    class Menu
    {
        public const int AddCommand = 1;
        public const int DeleteCommand = 2;
        public const int SearchNameCommand = 3;
        public const int SearchAuthorCommand = 4;
        public const int SearchYearCommand = 5;
        public const int ShowAllCommand = 6;
        public const int ExitCommand = 7;

        public void Show()
        {
            Console.WriteLine($"{AddCommand}. Добавить книгу.");
            Console.WriteLine($"{DeleteCommand}. Удалить книгу.");
            Console.WriteLine($"{SearchNameCommand}. Найти книгу по имени.");
            Console.WriteLine($"{SearchAuthorCommand}. Найти книгу по автору.");
            Console.WriteLine($"{SearchYearCommand}. Найти книгу по году.");
            Console.WriteLine($"{ShowAllCommand}. Показать все книги.");
            Console.WriteLine($"{ExitCommand}. Выход");
        }
    }

    class Utils
    {
        public int ReadInt(string prompt)
        {
            Console.WriteLine(prompt);

            bool isNumber = false;
            int number = 0;

            while (isNumber == false)
            {
                if (isNumber = int.TryParse(Console.ReadLine(), out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Вы ввели не число.");
                }
            }

            return number;
        }
    }
}
