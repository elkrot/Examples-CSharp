using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EnumsDemo
{
    enum BookBinding
    {
        None = 0,
        Hardcover,
        Paperback
    };

    enum BookLanguage
    {
        None = 0,
        [Culture("en-US")]
        [Culture("en-UK")]
        English = 1,
        [Culture("es-MX")]
        [Culture("es-ES")]
        Spanish = 2,
        [Culture("it-IT")]
        Italian = 3,
        [Culture("fr-FR")]
        [Culture("fr-BE")]
        French = 4,
    };

    [Flags]
    enum BookGenres
    {
        None = 0,
        ScienceFiction = 0x01,
        Crime = 0x02,
        Romance = 0x04,
        History = 0x08,
        Science = 0x10,
        Mystery = 0x20,
        Fantasy = 0x40,
        Vampire = 0x80,
    };

    class Program
    {
        static void Main(string[] args)
        {
            //print out various enum values
            BookBinding binding = BookBinding.Hardcover;
            BookBinding doubleBinding = BookBinding.Hardcover | BookBinding.Paperback;
            
            Console.WriteLine("Binding: {0}", binding);
            Console.WriteLine("Double Binding: {0}", doubleBinding);

            BookGenres genres = BookGenres.Vampire | BookGenres.Fantasy | BookGenres.Romance;
            Console.WriteLine("Genres: {0}", genres);

            BookBinding badBinding = (BookBinding)999;
            Console.WriteLine("badBinding ({0}) valid? {1}", badBinding, IsValidBinding(badBinding));

            Console.WriteLine("All BookGenre values:");
            foreach (BookGenres genre in Enum.GetValues(typeof(BookGenres)))
            {
                Console.WriteLine("\t" + Enum.GetName(typeof(BookGenres), genre));
            }

            //parse a string into an enum
            string hardcoverString = "hardcover";
            BookBinding hardcoverEnum = (BookBinding)Enum.Parse(typeof(BookBinding), hardcoverString, true);
            Console.WriteLine("Converting \"{0}\" to enumeration {1}", hardcoverString, hardcoverEnum);

            // Try to parse a bad string
            string badString = "Plasticwrap";
            try
            {
                Console.WriteLine("Trying to convert {0} to BookBinding enum", badString);
                BookBinding wontwork = (BookBinding)Enum.Parse(typeof(BookBinding), badString);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Use TryParse
            bool canParse = Enum.TryParse(hardcoverString, out hardcoverEnum);
            
            canParse = Enum.TryParse(badString, out badBinding);

            //parse a set of flags
            string flagString = "Vampire, Mystery, ScienceFiction, Vampire";
            BookGenres flagEnum = BookGenres.None;
            if (Enum.TryParse(flagString, out flagEnum))
            {
                Console.WriteLine("Parsed \"{0}\" into {1}", flagString, flagEnum);
            }
            
            //determine if flags are set
            Console.WriteLine("HasFlag(BookGenres.Vampire)? {0}", 
                flagEnum.HasFlag(BookGenres.Vampire));
            
            Console.WriteLine();
                       
            
            
            

            PrintCultures(BookLanguage.English);
            PrintCultures(BookLanguage.Spanish);

            Console.ReadKey();
        }

        static bool IsValidBinding(BookBinding binding)
        {
            return Enum.IsDefined(typeof(BookBinding), binding);
        }

        static void PrintCultures(BookLanguage language)
        {
            Console.WriteLine("Cultures for {0}:", language);
            foreach (string culture in language.GetCultures())
            {
                Console.WriteLine("\t" + culture);
            }
        }
    }
}
