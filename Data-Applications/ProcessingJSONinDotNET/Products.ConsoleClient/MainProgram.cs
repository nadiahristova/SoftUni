using Newtonsoft.Json;
using ProductsShop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Products.ConsoleClient
{
    public class MainProgram
    {
        static string folderPathAndName = @"../../../Output";
        static void Main()
        {
            var ctx = new ShopEntities();
            System.IO.Directory.CreateDirectory(folderPathAndName);

            //Quey 1
            var specificProducts = ctx.Products
                .Where(p => p.Buyer == null && p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                });

            var serializedProducts = JsonConvert.SerializeObject(specificProducts, Newtonsoft.Json.Formatting.Indented);
                       
            System.IO.File.WriteAllText(folderPathAndName + @"/products-in-range.json", serializedProducts);

            //Query 2
            var specificUsers = ctx.Users
                .Where(u => u.ProductsSold.Count() >= 1)
                .OrderBy(u => u.LastName)
                .OrderBy(u => u.FirstName)
                .Select(u => new 
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                        .Where(p => p.Buyer != null)
                                        .Select(p => new 
                                        {
                                            name = p.Name,
                                            price = p.Price,
                                            buyerFirstName = p.Buyer.FirstName,
                                            buyerLastName = p.Buyer.LastName
                                        })
                });

            var serializedUsers = JsonConvert.SerializeObject(specificUsers, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(folderPathAndName + @"/users-sold-products.json", serializedUsers);

            //Query 3
            var categories1 = ctx.Categories.ToArray();
            var categories = ctx.Categories
                .OrderByDescending(c => c.Products.Count())
                .Select( c => new 
                {
                    category = c.Name,
                    productsCount = c.Products.Count(),
                    averagePrice = c.Products.Count() == 0 ? 0 : c.Products.Average(p => p.Price),
                    totalRevenue = c.Products.Count() == 0 ? 0 : c.Products.Sum(p => p.Price)
                });

            var serializedCategories = JsonConvert.SerializeObject(categories, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(folderPathAndName + @"/categories-by-products.json", serializedCategories);

            //Query 4
            GenerateXmlWithData(folderPathAndName + @"/users-and-products.xml", ctx);
        }

        static void GenerateXmlWithData(string path, ShopEntities ctx)
        {
            var users = ctx.Users
                .Where(u => u.ProductsSold.Count() >= 1)
                .OrderByDescending(u => u.ProductsSold.Count())
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = u.ProductsSold
                                        .Select(p => new 
                                        {
                                            Name = p.Name,
                                            Price = p.Price
                                        })
                }).ToList();

            int usersCount = users.Count();

            var doc = new XDocument();

            var usersElement = new XElement("users");
            XAttribute attribute = new XAttribute("count", usersCount);
            usersElement.Add(attribute);

            foreach(var user in users)
            {
                XElement xmlUser = new XElement("user");

                if (user.FirstName != null)
                    xmlUser.Add(new XAttribute("first-name", user.FirstName));
                xmlUser.Add(new XAttribute("last-name", user.LastName));
                if (user.Age != null)
                    xmlUser.Add(new XAttribute("age", user.Age));

                var soldProducts = new XElement("sold-products");
                soldProducts.Add(new XAttribute("count", user.SoldProducts.Count()));

                if(user.SoldProducts.Count() != 0)
                    foreach (var product in user.SoldProducts)
                    {
                        XElement pr = new XElement("product", 
                            new XAttribute("name", product.Name),
                            new XAttribute("price", product.Price));

                        soldProducts.Add(pr);
                    }

                xmlUser.Add(soldProducts);

                usersElement.Add(xmlUser);
            }

            doc.Add(usersElement);

            doc.Save(path);
        }
    }
}
