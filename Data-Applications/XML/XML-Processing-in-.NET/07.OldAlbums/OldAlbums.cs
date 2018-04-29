using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace _07.OldAlbums
{
    class OldAlbums
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            XmlDocument doc = new XmlDocument();
            doc.Load("../../../01.Catalog.xml");

            Dictionary<string, decimal> oldAlbums = new Dictionary<string, decimal>();

            string xPath = @"albums/album";
            XmlNodeList albums = doc.SelectNodes(xPath);

            foreach(XmlNode album in albums)
            {
                int year = int.Parse(album["year"].InnerText);
                
                if(2015 - year > 5)
                {
                    string albumName = album["name"].InnerText;
                    decimal albumPrice = decimal.Parse(album["price"].InnerText);

                    oldAlbums[albumName] = albumPrice;
                }
            }

            foreach(var entry in oldAlbums)
            {
                Console.WriteLine("Album: {0}, price: {1} lv", entry.Key, entry.Value);
            }
        }
    }
}
