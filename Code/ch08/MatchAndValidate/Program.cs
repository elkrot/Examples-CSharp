using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MatchAndValidate
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("SSN");
            string[] ssnStrings = new string[]
            {
                "123456789",    //ok
                "123-45-6789",  //ok
                "111-11-1111",  //ok
                "123-45.678",   //bad
                "123.45.6789",  //bad
                "12.123.4444",  //bad
                "123.45.67890", //bad
                "123.a5.6789",  //bad
                "just random text", //bad
                
            };

            TestMatches("SSNs", ssnStrings, @"^\d{3}\-?\d{2}\-?\d{4}$");

            string[] phoneNumbers = new string[]
            {
                "123.456.7890", //ok
                "123-456-7890", //ok
                "1234567890",   //ok
                "123.4567890",  //ok
                "12.34.567.890",//ok
                "123.456.78900",//bad
                "123-456",      //bad
                "123-abc-7890"  //bad
            };

            TestMatches("Phone Numbers", phoneNumbers, 
                                                        //xxx.xxx.xxxx and xxx-xxx-xxxx
                                                        @"^((\d{3}[\-\.]?\d{3}[\-\.]?\d{4})|"+
                                                        //xx.xx.xxx.xxx and xx-xx-xxx-xxx
                                                        @"(\d{2}[\-\.]?\d{2}[\-\.]?\d{3}[\-\.]?\d{3}))"+
                                                        "$"
                                                       );
            string[] zipCodes = new string[]
            {
                "12345",    //ok
                
                "12345-6789",   //ok
                "123456789",    //ok
                "12345-",    //bad
                "1234",         //bad
                "1234-6789",    //bad
                "a234",         //bad
                "123456",       //bad
                "1234567890"    //bad
            };

            TestMatches("Zip codes", zipCodes, @"^\d{5}(-?\d{4})?$");

            //MM/DD/YYYY
            string[] dates = new string[]
            {
                "12/25/2009",   //ok
                "01/25/2009",   //ok
                "1/2/2009",     //bad (yeah, it's strict)
                "25/12/2009",   //bad
                "2009/12/25",   //bad
                "13/25/2009",   //bad
                "12/25/09",     //bad
            };

            TestMatches("Dates", dates, @"(0[1-9]|1[012])/([1-9]|0[1-9]|[12][0-9]|3[01])/\d{4}");

            Console.ReadKey();
        }

        static void TestMatches(string title, string[] checkList, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            Console.WriteLine("Checking {0} against pattern: {1}", title, pattern);
            foreach (string s in checkList)
            {
                Console.WriteLine("{0} ? {1}", s, regex.IsMatch(s)?"ok":"bad");
            }
        }
    }
}
