using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class Dictionary
    {
        public string Type { get; set; }
        public Dictionary<string, List<string>> Words { get; set; } = new Dictionary<string, List<string>>();


        public Dictionary() { } 

        public Dictionary(string type) { this.Type = type; }

        public Dictionary(string type, string words, List<string> translates)
        {
            Type = type;
            Words.Add(words, translates);
        }


        //public void Add(string word, List<string> translates)
        //{

        //}

        /// <summary>
        /// Добавляет слово и перевод в словарь
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="translate">Перевод</param>

        public void Add(string word, string translate)
        {
            if (!Words.ContainsKey(word))
            {
                Words.Add(word, new List<string>() { translate });
            }
            else
            {
               if (Words.ContainsKey(word) && !Words[word].Contains(translate))
               {
                  Words[word].Add(translate);
               } 
            }
        }

        /// <summary>
        /// Удаляет и его перевод из словаря
        /// </summary>
        /// <param name="word">Слово для удаления</param>
        public void DeleteWord(string word)
        {
            Words[word].Clear();
            Words.Remove(word);
        }

        /// <summary>
        /// Удалает перевод в указаном слове
        /// </summary>
        /// <param name="word">Слово из которого нужно удалить перевод</param>
        /// <param name="translate">Перевод, который нужно удалить</param>
        public void RemoveTranslate(string word, string translate)
        {
            if (Words[word].Count > 1)
            {
                Words[word].Remove(translate);
            }
        }

        /// <summary>
        /// Сохраняет словарь в файл
        /// </summary>
        public void Save()
        {
            using (FileStream fs = new FileStream(Type + ".txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    foreach(var words in Words)
                    {
                        sw.WriteLine(words.Key);
                        foreach(var item in words.Value)
                        { 
                            sw.WriteLine(' ' + item);
                        }
                        sw.WriteLine(" ");
                    }
                }
            }
        }
        /// <summary>
        /// "Достает" словарь из файла
        /// </summary>
        public void Load()
        {
            using(FileStream fileStream = new FileStream(Type + ".txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fileStream))
                {
                    while(!sr.EndOfStream)
                    {
                        var word = sr.ReadLine();
                        List<String> read = new List<String>();
                        string w = sr.ReadLine();
                        while(w != " " && w!= "" && w != null)
                        {
                            read.Add(w.Remove(0, 1));
                            w = sr.ReadLine();
                        }
                        foreach(var item in read)
                        {
                            Add(word, item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Меняет слово сохраняя переводы
        /// </summary>
        /// <param name="OldWord">Старое которое нужно изменить</param>
        /// <param name="NewWord">Слово на которое нужно заменить</param>
        /// <returns>Возврящает true если возможно заменить слово или
        ///false если такое слово уже есть(В таком случае слово не изменится)</returns>
        public bool ChangeWord(string OldWord, string NewWord)
        {
            bool check = false;
            List<string> TList = new List<string>();
            TList = Words[OldWord].ToList();
            foreach(var word in Words)
            {
                if (word.ToString().Equals(NewWord))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            if(check)
            {
                Words.Remove(OldWord);
                Words.Add(NewWord, TList);

                return check;
            }
            else
            {
                return check;
            }
        }
    }
}
