using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FormattingAndParsing
{
    class PrintNumbers
    {
        private static CultureInfo ci = CultureInfo.InvariantCulture;

        public static void Run()
        {
            Console.WriteLine("Original: 12345.6789");
            PrintNumberFormatted(12345.6789);

            Console.WriteLine("");
            Console.WriteLine("Original: 0.12345");
            PrintNumberFormatted(0.12345);

            Console.WriteLine("");
            Console.WriteLine("Original: 12345");
            PrintIntNumberFormatted(12345);

            Console.WriteLine("");
            Console.WriteLine("Original: 12345");
            PrintHex(12345);

            Console.WriteLine("");
            Console.WriteLine("Currency, Original: 12345");
            Console.WriteLine("Note: some currency symbols may not show up in your console, depending on your system");
            PrintCurrencies(12345);

            Console.WriteLine("");
            Console.WriteLine("Custom Format Strings");
            PrintCustomFormatStrings(12345.6789);
        }

        static void PrintNumberFormatted(double number)
        {
            Console.WriteLine("G: " + number.ToString("G", ci));
            Console.WriteLine("G4: " + number.ToString("G4", ci));
            Console.WriteLine("G5: " + number.ToString("G5", ci));

            Console.WriteLine("F: " + number.ToString("F", ci));
            Console.WriteLine("F6: " + number.ToString("F6", ci));

            Console.WriteLine("e: " + number.ToString("e", ci));
            Console.WriteLine("E: " + number.ToString("E", ci));
            Console.WriteLine("E3: " + number.ToString("E3", ci));

            Console.WriteLine("N: " + number.ToString("N", ci));
            Console.WriteLine("N0: " + number.ToString("N0", ci));
            Console.WriteLine("N5: " + number.ToString("N5", ci));

            Console.WriteLine("P: " + number.ToString("P", ci));
            Console.WriteLine("P1: " + number.ToString("P1", ci));

            Console.WriteLine("C: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
            Console.WriteLine("C3: " + number.ToString("C3", CultureInfo.CreateSpecificCulture("en-GB")));
        }

        static void PrintIntNumberFormatted(int number)
        {
            Console.WriteLine("D: " + number.ToString("D", ci));
            Console.WriteLine("D8: " + number.ToString("D8", ci));

            Console.WriteLine("F: " + number.ToString("F", ci));
            Console.WriteLine("F3: " + number.ToString("F3", ci));

            Console.WriteLine("E: " + number.ToString("E", ci));
            Console.WriteLine("E3: " + number.ToString("E3", ci));

            Console.WriteLine("X: " + number.ToString("X", ci));
            Console.WriteLine("X8: " + number.ToString("X8", ci));
        }

        private static void PrintHex(Int32 number)
        {
            Console.WriteLine("Hex: {0:X}", number);
            Console.WriteLine("Hex: 0x{0:X8}", number);
        }

        private static void PrintCurrencies(Int32 number)
        {
            Console.WriteLine("en-US: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
            Console.WriteLine("en-GB: " + number.ToString("C", CultureInfo.CreateSpecificCulture("en-GB")));
            Console.WriteLine("fr-FR: " + number.ToString("C", CultureInfo.CreateSpecificCulture("fr-FR")));
            Console.WriteLine("ja-JA: " + number.ToString("C", CultureInfo.CreateSpecificCulture("ja-JA")));
            Console.WriteLine("zh-HK: " + number.ToString("C", CultureInfo.CreateSpecificCulture("zh-HK")));
            Console.WriteLine("mn-MN: " + number.ToString("C", CultureInfo.CreateSpecificCulture("mn-MN")));

            double val = 1234567.89;
            Console.WriteLine(val.ToString("N", CultureInfo.CreateSpecificCulture("fr-FR")));
            Console.WriteLine(string.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N}", val));
        }

        private static void PrintCustomFormatStrings(double number)
        {
            Console.WriteLine(number.ToString("00000000.00", ci));
            Console.WriteLine(number.ToString("00,000,000.00", ci));
            Console.WriteLine(number.ToString("00,000,000.00", CultureInfo.CreateSpecificCulture("hi-IN")));
            double neg = number * -1;
            Console.WriteLine(neg.ToString("00,000,000.00;(00000000.000)", ci));
            double zero = 0.0;
            Console.WriteLine(zero.ToString("00,000,000.00;(00000000.000);'nothing!'", ci));
        }
    }
}
