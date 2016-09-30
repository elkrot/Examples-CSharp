// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Linq;
using LinqToTerraServerProvider;

[assembly: CLSCompliant(true)]
namespace ClientApp
{
    public sealed class Program
    {
        static void Main()
        {
            QueryableTerraServerData<Place> terraPlaces = new QueryableTerraServerData<Place>();

            // Первый запрос.
            var query1 = from place in terraPlaces
                        where place.Name== "Redmond"
                        select place.State;

            Console.WriteLine("States that have a place named \"Redmond\":");
            foreach (string name in query1)
                Console.WriteLine(name);

            // Второй запрос.
            var count = (from place in terraPlaces
                         where place.Name.StartsWith("Lond")
                         select place).Count();

            Console.WriteLine("\nNumber of places whose names start with \"Lond\": " + count + "\n");

            // Третий запрос.
            string[] places = { "Johannesburg", "Yachats", "Seattle" };

            var query3 = from place in terraPlaces
                        where places.Contains(place.Name)
                        orderby place.State
                        select new { place.Name, place.State };

            Console.WriteLine("Places, ordered by state, whose name is either \"Johannesburg\", \"Yachats\", or \"Seattle\":");
            foreach (var obj in query3)
                Console.WriteLine(obj);

            Console.ReadLine();
        }

        private Program() { }
    }
}
