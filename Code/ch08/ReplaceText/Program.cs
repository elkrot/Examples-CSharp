using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReplaceText
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "We few, we happy few, we band of brothers";
            Console.WriteLine("source: {0}", source);
            Console.WriteLine("Replace word after we");
            Regex regex = new Regex("[wW]e\\s[a-zA-Z]+");
            string result = regex.Replace(source, "we <something>");
            Console.WriteLine("result: {0}", result);

            Console.WriteLine("Swap we with next word:");
            //put the word after we into its own group so we can extract it later
            regex = new Regex("[wW]e\\s(?<OtherWord>[a-zA-Z]+)");
            //pass in our own method for the evaluator to use
            result = regex.Replace(source, new MatchEvaluator(SwapOrder));
            Console.WriteLine("result: {0}", result);

            Console.ReadKey();
        }

        static string SwapOrder(Match m)
        {
            //put whatever the other was first, then put " we"
            return m.Groups["OtherWord"].Value + " we";
        }
    }
}
