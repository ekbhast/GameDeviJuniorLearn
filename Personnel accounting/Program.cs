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
                "Гуров Тимофей Максимович",
                "Скворцова Ульяна Андреевна",
                "Богданов Андрей Кириллович",
                "Максимов Артём Робертович",
                "Герасимов Денис Андреевич",
                //"Гуров Иван Максимович",
                //"Сафонов Михаил Юрьевич",
                //"Моисеев Даниил Дмитриевич",
                //"Лебедева Василиса Максимовна",
                //"Коршунов Михаил Львович",
                //"Давыдов Андрей Никитич",
                //"Дубинина Таисия Адамовна",
                //"Куликов Артём Макарович",
                //"Николаева Полина Матвеевна",
                //"Плотников Дамир Артемьевич",
                //"Ушакова Амина Борисовна",
                //"Савельева Арина Родионовна",
                //"Ефимов Георгий Романович",
                //"Парфенов Давид Егорович",
                //"Попова Софья Александровна",
                //"Зубкова Софья Ивановна",
                //"Чижова Екатерина Ивановна",
                //"Васильева София Никитична",
                //"Серова Александра Александровна",
                //"Васильева Ярослава Кирилловна",
                //"Гуров Егор Валерьевич",
                //"Карташова Елизавета Львовна",
                //"Кузнецова Мария Павловна",
                //"Жилин Всеволод Максимович",
                //"Черкасов Артём Александрович"
            };
            string[] positions =
            {
                "Генеральный директор",
                "Исполнительный директор",
                "Технический директор",
                "Финансовый директор",
                "Коммерческий директор",
                //"Руководитель отдела разработки",
                //"Руководитель отдела продаж",
                //"Руководитель отдела маркетинга",
                //"Руководитель отдела персонала",
                //"Руководитель IT-отдела",
                //"Старший программист",
                //"Программист",
                //"Младший программист",
                //"Frontend-разработчик",
                //"Backend-разработчик",
                //"Fullstack-разработчик",
                //"Тестировщик",
                //"Ведущий тестировщик",
                //"Системный администратор",
                //"DevOps-инженер",
                //"Бизнес-аналитик",
                //"Системный аналитик",
                //"Проектный менеджер",
                //"Продуктовый менеджер",
                //"UX/UI дизайнер",
                //"Графический дизайнер",
                //"Контент-менеджер",
                //"SEO-специалист",
                //"Специалист технической поддержки",
                //"Офис-менеджер"
            };

            bool isExit = false;
            ConsoleColor defaultConsoleColor = Console.ForegroundColor;

            while(isExit == false)
            {
                ShowMainMenu();

                string userInput = Console.ReadLine();

                switch (userInput) 
                {
                    case "3":
                        Console.Clear();

                        ShowEmployees(FilterEmployees(employees), employees, positions);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Введите порядковый номер сотрудника которго необходимо удалить: ");

                        string inputIndex = Console.ReadLine();

                        int removeIndex;
                        bool isNumber = int.TryParse(inputIndex, out removeIndex);

                        if (isNumber && removeIndex >= 0 && removeIndex <= employees.Length)
                        {   
                            Console.WriteLine($"Вы выбрали сотрудника - {employees[removeIndex - 1]}");
                            DeleteEmployee(removeIndex, ref employees, ref positions);

                            Console.WriteLine($"Новый массив:");
                            ShowEmployees(FilterEmployees(employees), employees, positions);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели не число или порядковый номер которго не существует");
                        }

                        Console.ForegroundColor = defaultConsoleColor;
                        Console.Write('\n');
                            break;
                    case "2":
                        Console.Clear();

                        ShowEmployees(FilterEmployees(employees), employees, positions);

                        Console.Write('\n');
                        break;
                    case "5":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Выбран не существующий пункт меню");
                        break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            const string AddCommand = "1";
            const string ShowAllCommand = "2";
            const string DeleteCommand = "3";
            const string SearchLastnameCommand = "4";
            const string ExitCommand = "5";

            const string AddItemMenuName = "Добавить сотрудника";
            const string ShowAllItemMenuName = "Показать всех сотрудников";
            const string DeleteItemMenuName = "Удалить сотрудника";
            const string SearchLastnameItemMenuName = "Найти сотрудника  по фамилии";
            const string ExitItemMenuName = "Выход";

            Console.WriteLine($"{AddCommand}. {AddItemMenuName}");
            Console.WriteLine($"{ShowAllCommand}. {ShowAllItemMenuName}");
            Console.WriteLine($"{DeleteCommand}. {DeleteItemMenuName}");
            Console.WriteLine($"{SearchLastnameCommand}. {SearchLastnameItemMenuName}");
            Console.WriteLine($"{ExitCommand}. {ExitItemMenuName}");

        }

        private static void CreateEmployee() { }

        private static void ShowEmployees(int[] filteredIndex, string[] employees, string[] positions)
        {           
            for (int i = 0; i < filteredIndex.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {employees[filteredIndex[i]]} - {positions[filteredIndex[i]]}");
            }
        }

        private static void DeleteEmployee(int indexEmployee,  ref string [] employees, ref string[] positions) 
        {
            RemoveArrayItem(indexEmployee, ref employees);
            RemoveArrayItem(indexEmployee, ref positions);
        }

        private static void RemoveArrayItem(int removeIndex, ref string[] array)
        {
            int newIndex = 0;
            string[] tempArray = new string[array.Length - 1];
            for (int i = 0; i < array.Length; i++) 
            {
                if (i == removeIndex - 1) continue;

                tempArray[newIndex] = array[i];
                newIndex++;
            }

            array = tempArray;
        }

        private static int[] FilterEmployees(string[] employees, string filterLastname = "all")
        {
            int[] filteredIndex = new int[0];

            if (filterLastname == "all")
            {
                filteredIndex = new int[employees.Length];

                for (int i = 0; i < filteredIndex.Length; i++)
                {
                    filteredIndex[i] = i;
                }

                return filteredIndex;
            } 
            else
            {
                for (int i = 0; i <= employees.Length - 1 ; i++) 
                {
                    string lastname = employees[i].Split(' ')[0];

                    if (lastname == filterLastname)
                    {
                        
                        int[] tempFilteredIndex = new int[filteredIndex.Length + 1];

                        for (int j = 0; j < filteredIndex.Length - 1; j++) 
                        {
                            tempFilteredIndex[j] = filteredIndex[j];
                        }

                        tempFilteredIndex[tempFilteredIndex.Length - 1] = i; 

                        filteredIndex = tempFilteredIndex;
                    }
                }
                return filteredIndex;
            }
        }
    }
}
