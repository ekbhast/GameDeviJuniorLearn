using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_accounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] employees = 
            {
                "Казаков Тимофей Максимович",
                "Скворцова Ульяна Андреевна",
                "Богданов Андрей Кириллович",
                "Максимов Артём Робертович",
                "Герасимов Денис Андреевич",
                "Гуров Иван Максимович",
                "Сафонов Михаил Юрьевич",
                "Моисеев Даниил Дмитриевич",
                "Лебедева Василиса Максимовна",
                "Коршунов Михаил Львович",
                "Давыдов Андрей Никитич",
                "Дубинина Таисия Адамовна",
                "Куликов Артём Макарович",
                "Николаева Полина Матвеевна",
                "Плотников Дамир Артемьевич",
                "Ушакова Амина Борисовна",
                "Савельева Арина Родионовна",
                "Ефимов Георгий Романович",
                "Парфенов Давид Егорович",
                "Попова Софья Александровна",
                "Зубкова Софья Ивановна",
                "Чижова Екатерина Ивановна",
                "Васильева София Никитична",
                "Серова Александра Александровна",
                "Васильева Ярослава Кирилловна",
                "Абрамов Егор Валерьевич",
                "Карташова Елизавета Львовна",
                "Кузнецова Мария Павловна",
                "Жилин Всеволод Максимович",
                "Черкасов Артём Александрович"
            };
            string[] positions =
            {
                "Генеральный директор",
                "Исполнительный директор",
                "Технический директор",
                "Финансовый директор",
                "Коммерческий директор",
                "Руководитель отдела разработки",
                "Руководитель отдела продаж",
                "Руководитель отдела маркетинга",
                "Руководитель отдела персонала",
                "Руководитель IT-отдела",
                "Старший программист",
                "Программист",
                "Младший программист",
                "Frontend-разработчик",
                "Backend-разработчик",
                "Fullstack-разработчик",
                "Тестировщик",
                "Ведущий тестировщик",
                "Системный администратор",
                "DevOps-инженер",
                "Бизнес-аналитик",
                "Системный аналитик",
                "Проектный менеджер",
                "Продуктовый менеджер",
                "UX/UI дизайнер",
                "Графический дизайнер",
                "Контент-менеджер",
                "SEO-специалист",
                "Специалист технической поддержки",
                "Офис-менеджер"
            };

            ShowEmployees(FilterEmployees(employees), employees, positions);
        }

        private static void CreateEmployee() { }

        private static void ShowEmployees(int[] filteredIndex, string[] employees, string[] positions)
        {           
            for (int i = 0; i < filteredIndex.Length; i++)
            {
                Console.WriteLine($"{employees[filteredIndex[i]]} - {positions[filteredIndex[i]]}");
            }
        }

        private static void DeleteEmployee() { }

        private static int[] FilterEmployees(string[] employees, string filterValue = "all")
        {
            int[] filteredIndex;
           
                filteredIndex = new int[employees.Length];

                for (int i = 0; i < filteredIndex.Length; i++)
                {
                    filteredIndex[i] = i;
                }
                return filteredIndex;
            //if (filterValue == "all")
            //{
            //}
            //else
            //{
            //    for (int i = 0; i < employees.Length; i++)
            //    {

            //    }
        }
    }
}
