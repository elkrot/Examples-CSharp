using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FormattingAndParsing
{
    class ParseNumbers
    {
        public static void Run()
        {
            //TryParse--ok
            string goodStr = " -100,000,000.567 ";
            double goodVal = 0;
            if (double.TryParse(goodStr, out goodVal))
            {
                Console.WriteLine("Parsed {0} to number {1}", goodStr, goodVal);
            }

            //TryParse--narrow format
            if (!double.TryParse(goodStr, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out goodVal))
            {
                Console.WriteLine("Unable to parse {0} with limited NumberStyle", goodStr);
            }
            
            //TryParse--fail
            string badStr = "-100 100 100,987";
            double badVal = 0;
            if (!double.TryParse(badStr, out badVal))
            {
                Console.WriteLine("Unable to parse {0} into number", badStr);
            }
            
            //TryParse--different culture
            string frStr = "-100 100 100,987";
            double frVal = 0;
            if (double.TryParse(frStr, NumberStyles.Any, CultureInfo.CreateSpecificCulture("fr-FR"), out frVal))
            {
                Console.WriteLine("Parsed {0} ({1}) into {2}", frStr, "fr-FR", frVal);
            }

            //TryParse with hex
            string hexStr = "0x3039";
            Int32 hexVal = 0;
            if (Int32.TryParse(hexStr.Replace("0x",""), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out hexVal))
            {
                Console.WriteLine("Parsed {0} to value {1}", hexStr, hexVal);
            }

        }
    }
}
