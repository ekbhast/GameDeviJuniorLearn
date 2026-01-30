using System;

namespace has_a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Performer worker1 = new Performer("Василий");
            Performer worker2 = new Performer("Геннадий");

            Task[] tasks = {
                new Task(worker1, "работать до рассвета"),
                new Task(worker2, "работать до темноты"),
            };

            Board schefule = new Board(tasks);

            schefule.SHowAllTasks();
        }
    }

    class Performer
    {
        public string Name;

        public Performer(string name)
        {
            Name = name;
        }
    }

    class Task
    {
        public Performer Worker;
        public string Description;

        public Task(Performer worker, string description)
        {
            Worker = worker;
            Description = description;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Рабочий {Worker.Name}, задача {Description}");
        }
    }

    class Board
    {
        public Task[] Tasks;
        public Board(Task[] tasks)
        {
            Tasks = tasks;
        }

        public void SHowAllTasks()
        {
            foreach (var task in Tasks)
            {
                task.ShowInfo();
            }
        }
    }
}
