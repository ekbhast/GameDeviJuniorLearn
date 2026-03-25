using System;
using System.Collections.Generic;
using System.Linq;

namespace Transfer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> firstSoldiersSquad = new List<string>
            {
                "Петров Иван",
                "Смирнов Алексей",
                "Иванов Дмитрий",
                "Кузнецов Сергей",
                "Соколов Андрей",
                "Васильев Николай",
                "Беляев Антон",
                "Фёдоров Евгений",
                "Михайлов Владимир",
                "Барабанов Алексей",
            };

            List<string> secondSoldiersSquad = new List<string>
            {
                "Морозов Павел",
                "Волков Игорь",
                "Павлов Олег",
                "Орлов Константин",
                "Андреев Роман",
                "Сергеев Виктор",
                "Захаров Юрий",
                "Попов Михаил",
                "Новиков Александр",
                "Никитин Артём"
            };

            SquadService service = new(firstSoldiersSquad, secondSoldiersSquad);
            service.Run();
        }
    }

    public class SquadService
    {
        private List<string> _firstSoldiersSquad = new();
        private List<string> _secondSoldiersSquad = new();

        public SquadService(List<string> first, List<string> second)
        {
            _firstSoldiersSquad = first.ToList();
            _secondSoldiersSquad = second.ToList();    
        }

        public void Run()
        {
            Console.WriteLine("Изначальные списки:\n");

            Console.WriteLine("Первый отряд:");
            ShowListSoldier(_firstSoldiersSquad);

            Console.WriteLine("\nВторой отряд:");
            ShowListSoldier(_secondSoldiersSquad);

            Transfer();

            Console.WriteLine("\nПервый отряд после перевода:");
            ShowListSoldier(_firstSoldiersSquad);

            Console.WriteLine("\nВторой отряд после перевода:");
            ShowListSoldier(_secondSoldiersSquad);
        }

        private void Transfer()
        {
            _secondSoldiersSquad = _secondSoldiersSquad.Union(_firstSoldiersSquad.Where(s => s.StartsWith('Б'))).ToList();
            _firstSoldiersSquad = _firstSoldiersSquad.Where(soldier => !soldier.StartsWith('Б')).ToList();
        }

        private void ShowListSoldier(List<string> soldiers)
        {
            foreach (var name in soldiers)
            {
                Console.WriteLine(name);
            }
        }
    }
}