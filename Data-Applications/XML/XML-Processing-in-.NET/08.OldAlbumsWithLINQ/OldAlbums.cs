using System;
using System.Linq;
using System.Xml.Linq;

namespace OldAlbumsWithLINQ
{
    class OldAlbums
    {
        static void Main()
        {
            XDocument doc = XDocument.Load("../../../01.Catalog.xml");

            var albums =
                from album in doc.Descendants("album")
                where int.Parse(album.Element("year").Value) <= 2000
                select new
                {
                    Title = album.Element("name").Value,
                    Price = album.Element("price").Value
                };
            

            foreach(var album in albums)
            {
                Console.WriteLine("Album: {0}, price: {1}", album.Title, album.Price);
            }
        }
    }
}
