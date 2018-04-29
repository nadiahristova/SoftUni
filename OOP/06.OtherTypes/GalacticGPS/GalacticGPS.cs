namespace GalacticGPS
{
    using Enums;
    using Structs;

    using System;

    class GalacticGPS
    {
        static void Main()
        {
            Location home = new Location(18.037986, -199, Planet.Earth);

            Console.WriteLine(home);
        }
    }
}
