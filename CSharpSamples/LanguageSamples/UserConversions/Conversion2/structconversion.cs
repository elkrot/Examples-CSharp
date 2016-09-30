// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// structconversion.cs
using System;

struct RomanNumeral
{
    public RomanNumeral(int value) 
    {
        this.value = value; 
    }
    static public implicit operator RomanNumeral(int value)
    {
        return new RomanNumeral(value);
    }
    static public implicit operator RomanNumeral(BinaryNumeral binary)
    {
        return new RomanNumeral((int)binary);
    }
    static public explicit operator int(RomanNumeral roman)
    {
         return roman.value;
    }
    static public implicit operator string(RomanNumeral roman) 
    {
        return("Conversion not yet implemented");
    }
    private int value;
}

struct BinaryNumeral
{
    public BinaryNumeral(int value) 
    {
        this.value = value;
    }
    static public implicit operator BinaryNumeral(int value)
    {
        return new BinaryNumeral(value);
    }
    static public implicit operator string(BinaryNumeral binary)
    {
        return("Conversion not yet implemented");
    }
    static public explicit operator int(BinaryNumeral binary)
    {
        return(binary.value);
    }

    private int value;
}

class Test
{
    static public void Main()
    {
        RomanNumeral roman;
        roman = 10;
        BinaryNumeral binary;
        // Выполнение преобразования типа RomanNumeral в
        // BinaryNumeral:
        binary = (BinaryNumeral)(int)roman;
        // Выполнение преобразования типа BinaryNumeral в RomanNumeral.
        // Приведение типов не требуется:
        roman = binary;
        Console.WriteLine((int)binary);
        Console.WriteLine(binary);
    }
}

