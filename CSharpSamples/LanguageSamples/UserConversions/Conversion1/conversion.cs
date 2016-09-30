// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// conversion.cs
using System;

struct RomanNumeral
{
    public RomanNumeral(int value) 
    { 
       this.value = value; 
    }
    // Объявление преобразования типа int в RomanNumeral. Обратите внимание на
    // использование ключевого слова оператора. Речь идет об операторе 
    // преобразования с именем RomanNumeral:
    static public implicit operator RomanNumeral(int value) 
    {
       // Следует заметить, что поскольку RomanNumeral объявлен как структура, 
       // то вызов оператора new структуры -- это фактически вызов конструктора, 
       // а не выделение объекту памяти в куче:
       return new RomanNumeral(value);
    }
    // Объявление явного преобразования типа RomanNumeral в int:
    static public explicit operator int(RomanNumeral roman)
    {
       return roman.value;
    }
    // Объявление неявного преобразования типа RomanNumeral в 
    // строку:
    static public implicit operator string(RomanNumeral roman)
    {
       return("Conversion not yet implemented");
    }
    private int value;
}

class Test
{
    static public void Main()
    {
        RomanNumeral numeral;

        numeral = 10;

// Вызов явного преобразования числового типа в int. Поскольку речь идет
// о явном преобразовании, необходимо использовать приведение типов:
        Console.WriteLine((int)numeral);

// Вызов неявного преобразования в строку. Поскольку в этом случае нет
// приведения типов, неявное преобразование в строку -- это единственно
// возможный тип преобразования.
        Console.WriteLine(numeral);
 
// Вызов явного преобразования числового типа в int, а 
// затем явного преобразования типа int в short:
        short s = (short)numeral;

        Console.WriteLine(s);
    }
}

