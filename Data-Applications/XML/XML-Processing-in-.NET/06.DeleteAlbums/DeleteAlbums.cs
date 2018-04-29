using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DeleteAlbums
{
    class DeleteAlbums
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            XmlDocument doc = new XmlDocument();
            doc.Load("../../../01.Catalog.xml");

            XmlNode rootNode = doc.DocumentElement;
            foreach (XmlNode node in rootNode)
            {
                double price = double.Parse(node["price"].InnerText);

                if (price > 20)
                {
                    node.RemoveAll();
                }
            }

            doc.Save("../../../cheap-albums-catalog.xml");
        }
    }
}
