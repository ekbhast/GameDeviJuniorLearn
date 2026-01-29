using System;

namespace lectures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Table[] tables = { new Table(1, 4), new Table(2, 8), new Table(3, 10) };

            bool isOpen = true;

            while (isOpen == true)
            {
                Console.WriteLine("Администрирование кафе. \n");

                foreach (Table table in tables)
                {
                    table.ShowInfo();
                }

                Console.WriteLine("Введите номер стола: ");
                int wishTable = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("Введите количество мест для брони:");
                int disiredPlaces = Convert.ToInt32(Console.ReadLine());

                bool isReservationCompleted = tables[wishTable].Reserve(disiredPlaces); 

                if (isReservationCompleted == true)
                {
                    Console.WriteLine("Бронь прошла успешно");
                }
                else
                {
                    Console.WriteLine("Бронь не пршла. Не достаточно мест");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Table
    {
        public int Number;
        public int MaxPlaces;
        public int FreePlaces;

        public Table(int number, int maxPlaces)
        {
            Number = number;
            MaxPlaces = maxPlaces;
            FreePlaces = maxPlaces;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Стол: {Number}, свободно мест: {FreePlaces}, из {MaxPlaces} ");
        }

        public bool Reserve(int places)
        {
            if (FreePlaces >= places)
            {
                FreePlaces -= places;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
