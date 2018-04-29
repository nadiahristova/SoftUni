using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtractAllArtistsAlphabetically
{
    class ExtractAllArtistsAlphabetically
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../01.Catalog.xml");

            XmlNode root = doc.DocumentElement;
            var artists = new SortedSet<string>();

            foreach (XmlNode node in root)
            {
                string artist = node["artist"].InnerText;
                artists.Add(artist);
            }

            foreach (var artist in artists)
            {
                Console.WriteLine(artist);
            }
        }
    }
}
