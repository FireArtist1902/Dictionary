using P11_CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    static class Prog
    {
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("Введите нужный Вам тип словаря(Пример: англо-русский)");
                string type = Console.ReadLine();
                Console.Clear();
                Work(type);
            }
        }

        static public int Work(string type)
        {
            while (true)
            {
                Console.WriteLine(type);
                Console.WriteLine();
                Console.WriteLine("Навигация: ");
                Console.WriteLine("Стрелки вверх и вниз для перемещения курсора");
                Console.WriteLine("Enter для выбора");
                int choice = ConsoleMenu.SelectVertical(HPosition.Center, VPosition.Center, HorizontalAlignment.Center, "Добавить слово/перевод", "Удалить перевод", "Удалить слово", "Изменить слово", "Найти перевод", "Изменить тип");
                switch (choice)
                {
                    case 0: AddToDictionary(type); break;
                    case 1: DeleteTranslateFromDictionary(type); break;
                    case 2: DeleteWordFromDictionary(type); break;
                    case 3: ChangeWord(type); break;
                    case 4: FindTranslate(type); break;
                    case 5: return 0;
                }
            }
        }

        static private int FindTranslate(string type)
        {
            Console.WriteLine("Введите слово перевод(переводы) которого нужно найти(0 для перехода назад): ");
            string word = Console.ReadLine();
            if (word == "0")
            {
                Console.Clear();
                return 0;
            }
            Dictionary dictionary = new Dictionary(type);
            dictionary.Load();
            foreach(var items in dictionary.Words[word])
            {
                Console.WriteLine(items);
            }
            dictionary.Save();
            Console.WriteLine("Нажмите Enter для продолжения");
            Console.ReadLine();
            Console.Clear();
            return 0;
        }

        static private int ChangeWord(string type)
        {
            Console.WriteLine("Введите слово, которое нужно изменить(0 для перехода назад): ");
            string Oldword = Console.ReadLine();
            if (Oldword == "0")
            {
                Console.Clear();
                return 0;
            }
            Console.WriteLine("Введите слово на которое нужно изменить: ");
            string NewWorld = Console.ReadLine();
            Dictionary dictionary = new Dictionary(type);
            dictionary.Load();
            dictionary.ChangeWord(Oldword, NewWorld);
            dictionary.Save();
            Console.Clear();
            return 0;
        }

        public static int DeleteWordFromDictionary(string type)
        {
            Console.WriteLine("Введите слово которое нужно удалить(0 для перехода назад): ");
            string word = Console.ReadLine();
            if (word == "0")
            {
                Console.Clear();
                return 0;
            }
            Dictionary dictionary = new Dictionary(type);
            dictionary.Load();
            dictionary.DeleteWord(word);
            dictionary.Save();
            Console.Clear();
            return 0;
        }

        static private int AddToDictionary(string type)
        {
            Console.WriteLine("Введите слово, которое нужно добавить в словарь или слово к которому нужно добавить перевод(0 для перехода назад): ");
            string word = Console.ReadLine();
            if (word == "0")
            {
                Console.Clear();
                return 0;
            }
            Console.WriteLine("Введите перевод: ");
            string translate = Console.ReadLine();
            Dictionary dictionary = new Dictionary(type);
            dictionary.Load();
            dictionary.Add(word, translate);
            dictionary.Save();
            Console.Clear();
            return 0;
        }
        
        static private int DeleteTranslateFromDictionary(string type)
        {
            Console.WriteLine("Введите слово, перевод которого нужно удалить: ");
            string word = Console.ReadLine();
            if (word == "0")
            {
                Console.Clear();
                return 0;
            }

            Console.WriteLine("Введите перевод: ");
            string translate = Console.ReadLine();
            Dictionary dictionary = new Dictionary(type);
            dictionary.Load();
            dictionary.RemoveTranslate(word, translate);
            dictionary.Save();
            Console.Clear();
            return 0;
        }
    }
}
