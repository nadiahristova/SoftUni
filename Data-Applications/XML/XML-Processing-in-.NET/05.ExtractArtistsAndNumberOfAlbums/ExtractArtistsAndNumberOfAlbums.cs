using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtractArtistsAndNumberOfAlbums
{
    class ExtractArtistsAndNumberOfAlbums
    {
        static void Main()
        {
            var artistAlbumCount = new Dictionary<string, int>();

            XmlDocument doc = new XmlDocument();
            doc.Load("../../../01.Catalog.xml");

            string XPathArtist = "/albums/album/artist";

            XmlNodeList artistList = doc.SelectNodes(XPathArtist);

            foreach (XmlNode node in artistList)
            {
                string artist = node.InnerText;

                if (artistAlbumCount.ContainsKey(artist))
                {
                    artistAlbumCount[artist]++;
                }
                else
                {
                    artistAlbumCount[artist] = 1;
                }
            }

            foreach (KeyValuePair<string, int> keyValuePair in artistAlbumCount)
            {
                Console.WriteLine("{0} --- {1} albums", keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}
