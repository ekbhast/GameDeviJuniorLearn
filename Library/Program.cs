using System;
using System.Collections.Generic;

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
        private Utils _utils = new Utils();

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

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();

                menu.Show();

                int command = _utils.ReadInt("\nВыберите пункт меню");

                switch (command)
                {
                    case Menu.AddCommand:
                        AddBook();
                        break;

                    case Menu.DeleteCommand:
                        DeleteBook();
                        break;

                    case Menu.SearchNameCommand:
                        ShowBooks(SearchByName());
                        break;

                    case Menu.SearchAuthorCommand:
                        ShowBooks(SearchByAuthor());
                        break;

                    case Menu.SearchYearCommand:
                        ShowBooks(SearchByYear());
                        break;

                    case Menu.ShowAllCommand:
                        ShowBooks(_books);
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

        public void AddBook()
        {
            Console.WriteLine("Введите автора:");
            string author = Console.ReadLine();

            Console.WriteLine("Введите название книги:");
            string name = Console.ReadLine();

            int year = _utils.ReadInt("Введите год выпуска книги:");

            _books.Add(new Book(name, author, year));

            Console.WriteLine("Книга добавлена.");
        }

        public void DeleteBook()
        {
            Console.Clear();

            ShowBooks(_books);

            int index = _utils.ReadInt("Введите номер книги которую хотите удалить:") - 1;

            if (index > 0 && index <= _books.Count)
            {
                _books.RemoveAt(index);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Вы ввели номер не существющей книги!");
            }
        }

        public void ShowBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Ни чего не найдено");
            }
            else
            {
                for (int i = 0;  i < books.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {books[i].Author}, {books[i].Name}, {books[i].Year}");
                }
            }
        }

        public List<Book> SearchByAuthor()
        {
            List<Book> filteredBooks = new List<Book>();

            Console.Write("Введите автора: ");
            string userInput = Console.ReadLine();

            foreach (Book book in _books) 
            { 
                if (book.Author == userInput)
                {
                    filteredBooks.Add(book);
                }
            }

            return filteredBooks;
        }

        public List<Book> SearchByYear()
        {
            List<Book> filteredBooks = new List<Book>();

            int userInput = _utils.ReadInt("Введите год");

            foreach (Book book in _books)
            {
                if (book.Year == userInput)
                {
                    filteredBooks.Add(book);
                }
            }

            return filteredBooks;
        }

        public List<Book> SearchByName()
        {
            List<Book> filteredBooks = new List<Book>();

            Console.Write("Введите название книги: ");
            string userInput = Console.ReadLine();

            foreach (Book book in _books)
            {
                if (book.Name == userInput)
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