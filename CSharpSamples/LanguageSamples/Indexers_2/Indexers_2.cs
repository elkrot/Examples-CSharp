// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

// indexedproperty.cs
using System;

public class Document
{
    // Тип, позволяющий просматривать документ в виде массива слов:
    public class WordCollection
    {
        readonly Document document;  // Документ-контейнер

        internal WordCollection(Document d)
        {
           document = d;
        }

        // Вспомогательная функция -- поиск в символьном массиве "text", начиная с
        // символа "begin", число слов -- "wordCount". Возвращает значение false,
        // если число имеющихся слов меньше, чем wordCount. Задание значений "start" и
        // "length" для позиции и длины слова в пределах текста:
        private bool GetWord(char[] text, int begin, int wordCount, 
                                       out int start, out int length) 
        { 
            int end = text.Length;
            int count = 0;
            int inWord = -1;
            start = length = 0; 

            for (int i = begin; i <= end; ++i) 
            {
                bool isLetter = i < end && Char.IsLetterOrDigit(text[i]);

                if (inWord >= 0) 
                {
                    if (!isLetter) 
                    {
                        if (count++ == wordCount) 
                        {
                            start = inWord;
                            length = i - inWord;
                            return true;
                        }
                        inWord = -1;
                    }
                }
                else 
                {
                    if (isLetter)
                        inWord = i;
                }
            }
            return false;
        }

        // Индексатор для получения и задания слов  документа-контейнера:
        public string this[int index] 
        {
            get 
            { 
                int start, length;
                if (GetWord(document.TextArray, 0, index, out start, 
                                                          out length))
                    return new string(document.TextArray, start, length);
                else
                    throw new IndexOutOfRangeException();
            }
            set 
            {
                int start, length;
                if (GetWord(document.TextArray, 0, index, out start, 
                                                         out length)) 
                {
                    // Замените слово в позиции start/length на 
                    // строку "value":
                    if (length == value.Length) 
                    {
                        Array.Copy(value.ToCharArray(), 0, 
                                 document.TextArray, start, length);
                    }
                    else 
                    {
                        char[] newText = 
                            new char[document.TextArray.Length + 
                                           value.Length - length];
                        Array.Copy(document.TextArray, 0, newText, 
                                                        0, start);
                        Array.Copy(value.ToCharArray(), 0, newText, 
                                             start, value.Length);
                        Array.Copy(document.TextArray, start + length,
                                   newText, start + value.Length,
                                  document.TextArray.Length - start
                                                            - length);
                        document.TextArray = newText;
                    }
                }                    
                else
                    throw new IndexOutOfRangeException();
            }
        }

        // Получение числа слов в документе-контейнере:
        public int Count 
        {
            get 
            { 
                int count = 0, start = 0, length = 0;
                while (GetWord(document.TextArray, start + length, 0, 
                                              out start, out length))
                    ++count;
                return count; 
            }
        }
    }

    // Тип для просмотра документа в виде "массива" 
    // символов:
    public class CharacterCollection
    {
        readonly Document document;  // Документ-контейнер

        internal CharacterCollection(Document d)
        {
          document = d; 
        }

        // Индексатор для получения и задания символов в документе-контейнере:
        public char this[int index] 
        {
            get 
            { 
                return document.TextArray[index]; 
            }
            set 
            { 
                document.TextArray[index] = value; 
            }
        }

        // Получение числа символов в документе-контейнере:
        public int Count 
        {
            get 
            { 
                return document.TextArray.Length; 
            }
        }
    }

    // Поскольку у типов полей есть индексаторы, 
    // эти поля отображаются как "индексированные свойства":
    public WordCollection Words;
    public CharacterCollection Characters;

    private char[] TextArray;  // Текст документа. 

    public Document(string initialText)
    {
        TextArray = initialText.ToCharArray();
        Words = new WordCollection(this);
        Characters = new CharacterCollection(this);
    }

    public string Text 
    {
        get 
        { 
           return new string(TextArray); 
        }
    }
}

class Test
{
    static void Main()
    {
        Document d = new Document(
           "peter piper picked a peck of pickled peppers. How many pickled peppers did peter piper pick?"
        );

        // Замена слова "peter" на "penelope":
        for (int i = 0; i < d.Words.Count; ++i) 
        {
            if (d.Words[i] == "peter") 
                d.Words[i] = "penelope";
        }

        // Замена символа "p" на "P"
        for (int i = 0; i < d.Characters.Count; ++i) 
        {
            if (d.Characters[i] == 'p')
                d.Characters[i] = 'P';
        }
        
        Console.WriteLine(d.Text);
    }
}

