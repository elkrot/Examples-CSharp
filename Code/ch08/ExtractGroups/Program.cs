using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = 
                        "1234 Cherry Lane, Smalltown, USA" + Environment.NewLine + 
                        "1235 Apple Tree Drive, Smalltown, USA"+ Environment.NewLine + 
                        "3456 Cherry Orchard Street, Smalltown, USA" + Environment.NewLine;

            Regex regex = new Regex("^(?<HouseNumber>\\d+)\\s*(?<Street>[\\w\\s]*), (?<City>[\\w]+), (?<Country>[\\w\\s]+)$", RegexOptions.Multiline);
            MatchCollection coll = regex.Matches(file);
            foreach (Match m in coll)
            {
                string street = m.Groups["Street"].Value;
                Console.WriteLine("Street: {0}", street);
            }

            Console.ReadKey();

        }
    }
}
