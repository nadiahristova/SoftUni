using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtractAlbumNames_DOMParser
{
    class ExtractAlbumNames
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../01.Catalog.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root)
            {
                Console.WriteLine("Album: {0}", node["name"].InnerText);
            }           
        }
    }
}
