using System;

namespace ReverseWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string original =
                "But, in a larger sense, we can not dedicate—we can not consecrate—we can not hallow—this ground.";

            Console.WriteLine("Original: " + original);
            Console.WriteLine("Reversed: " + ReverseWords(original));

            Console.ReadKey();
        }

        static string ReverseWords(string original)
        {
            //first convert the string to a character array since we'll be needing to modify it extensively.
            char[] chars = original.ToCharArray();
            ReverseCharArray(chars, 0, chars.Length - 1);

            //now find consecutive characters and reverse each group individually
            int wordStart = 0;
            while (wordStart < chars.Length)
            {
                //skip past non-letters
                while (wordStart < chars.Length-1 && !char.IsLetter(chars[wordStart]))
                    wordStart++;
                //find end of letters
                int wordEnd = wordStart;
                while (wordEnd < chars.Length-1 && char.IsLetter(chars[wordEnd+1]))
                    wordEnd++;
                //reverse this range
                if (wordEnd > wordStart)
                {
                    ReverseCharArray(chars, wordStart, wordEnd);
                }
                wordStart = wordEnd + 1;
            }
            return new string(chars);
        }

        static void ReverseCharArray(char[] chars, int left, int right)
        {
            int l = left, r = right;
            while (l < r)
            {
                char temp = chars[l];
                chars[l] = chars[r];
                chars[r] = temp;
                l++;
                r--;
            }
        }
    }
}
