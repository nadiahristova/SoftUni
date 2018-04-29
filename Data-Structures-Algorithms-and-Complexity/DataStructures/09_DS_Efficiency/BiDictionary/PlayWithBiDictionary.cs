using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiDictionary
{
    class PlayWithBiDictionary
    {
        static void Main()
        {
            var distances = new BiDictionary<string, string, int>();
            distances.Add("Sofia", "Varna", 443);
            distances.Add("Sofia", "Varna", 468);
            distances.Add("Sofia", "Varna", 490);
            distances.Add("Sofia", "Plovdiv", 145);
            distances.Add("Sofia", "Bourgas", 383);
            distances.Add("Plovdiv", "Bourgas", 253);
            distances.Add("Plovdiv", "Bourgas", 292);

            var distancesFromSofia = distances.FindByKey1("Sofia"); // [443, 468, 490, 145, 383]
            Console.WriteLine("Find by first key \"Sofia\" should be [443, 468, 490, 145, 383]\n" + Print(distancesFromSofia));

            var distancesToBourgas = distances.FindByKey2("Bourgas"); // [383, 253, 292]
            Console.WriteLine("Find by second key \"Bourgas\" should be [383, 253, 292]\n" + Print(distancesToBourgas));

            var distancesPlovdivBourgas = distances.Find("Plovdiv", "Bourgas"); // [253, 292]
            Console.WriteLine("Find by keys \"Plovdiv\" and \"Bourgas\" should be [253, 292]\n" + Print(distancesPlovdivBourgas));

            var distancesRousseVarna = distances.Find("Rousse", "Varna"); // []
            Console.WriteLine("Find by keys \"Rousse\" and \"Varna\" should be []\n" + Print(distancesRousseVarna));

            var distancesSofiaVarna = distances.Find("Sofia", "Varna"); // [443, 468, 490]
            Console.WriteLine("Find by keys \"Sofia\" and \"Varna\" should be [443, 468, 490]\n" + Print(distancesSofiaVarna));

            distances.Remove("Sofia", "Varna"); // true

            var distancesFromSofiaAgain = distances.FindByKey1("Sofia"); // [145, 383]
            Console.WriteLine("Find by first key \"Sofia\" should be [145, 383]\n" + Print(distancesFromSofiaAgain));

            var distancesToVarna = distances.FindByKey2("Varna"); // []
            Console.WriteLine("Find by second key \"Varna\" should be []\n" + Print(distancesToVarna));

            var distancesSofiaVarnaAgain = distances.Find("Sofia", "Varna"); // []
            Console.WriteLine("Find by keys \"Sofia\" and \"Varna\" should be []\n" + Print(distancesSofiaVarnaAgain));
        }

        private static string Print(IEnumerable<int> distances)
        {
            return "[" + string.Join(", ", distances) + "]";
        }
    }
}
