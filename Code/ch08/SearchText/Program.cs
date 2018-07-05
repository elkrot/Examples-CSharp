using System;
using System.Text.RegularExpressions;

namespace SearchText
{
    class Program
    {
        static void Main(string[] args)
        {
            string source =
                "Four score and seven years ago our fathers brought forth on this continent, a new nation, conceived in Liberty, and dedicated to the proposition that all men are created equal." +
                Environment.NewLine +
                "Now we are engaged in a great civil war, testing whether that nation, or any nation so conceived and so dedicated, can long endure. We are met on a great battle-field of that war. We have come to dedicate a portion of that field, as a final resting place for those who here gave their lives that that nation might live. It is altogether fitting and proper that we should do this." +
                Environment.NewLine +
                "But, in a larger sense, we can not dedicate -- we can not consecrate -- we can not hallow -- this ground. The brave men, living and dead, who struggled here, have consecrated it, far above our poor power to add or detract. The world will little note, nor long remember what we say here, but it can never forget what they did here. It is for us the living, rather, to be dedicated here to the unfinished work which they who fought here have thus far so nobly advanced. It is rather for us to be here dedicated to the great task remaining before us -- that from these honored dead we take increased devotion to that cause for which they gave the last full measure of devotion -- that we here highly resolve that these dead shall not have died in vain -- that this nation, under God, shall have a new birth of freedom -- and that government of the people, by the people, for the people, shall not perish from the earth."
                + Environment.NewLine;

            Console.WriteLine("Source: " + source);

            Console.WriteLine("Find all 'we's:");
            Regex regex = new Regex("we");
            MatchCollection coll = regex.Matches(source);
            PrintMatchCollection(coll);

            Console.WriteLine("Find words 10 letters or longer:");
            PrintMatchCollection(new Regex("[a-zA-Z]{10,}").Matches(source));

            Console.WriteLine("Find the last word of each paragraph");
            PrintMatchCollection(new Regex("[\\w]+[\\.\\?\\!]?[\\n\\r]{1,}").Matches(source));

            Console.WriteLine("All capitalized words:");
            PrintMatchCollection(new Regex("[A-Z][a-z]*").Matches(source));

            Console.ReadKey();

        }

        static void PrintMatchCollection(MatchCollection coll)
        {
            foreach (Match match in coll)
            {
                Console.WriteLine("\t\"{0}\" at position {1}", match.Value.Trim(), match.Index);
            }
        }
    }
}
