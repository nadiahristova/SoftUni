namespace ProductsShop.Data.Migrations
{
    using Newtonsoft.Json;
    using ProductsShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class Configuration : DbMigrationsConfiguration<ProductsShop.Data.ShopEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProductsShop.Data.ShopEntities context)
        {
            using(var ctx = new ShopEntities())
            {
                Random rnd = new Random();

                 if(ctx.Users.Count() == 0)
                {
                    XmlDocument usersDoc = new XmlDocument();
                    usersDoc.Load(@"../../Resources/users.xml");
                     string xPath = @"users/user";
                     var users = usersDoc.SelectNodes(xPath);

                     foreach(XmlNode user in users)
                     {
                         User newUser = new User()
                         {
                             FirstName = user.Attributes["first-name"] == null ? null : user.Attributes["first-name"].Value,
                             LastName = user.Attributes["last-name"].Value,
                             Age = (user.Attributes["age"] == null ? (int?)null : int.Parse(user.Attributes["age"].Value))
                         };
                         ctx.Users.AddOrUpdate(newUser);
                     }        
                     
                }

                 if (ctx.Categories.Count() == 0)
                 {
                     using (StreamReader r = new StreamReader(@"../../Resources/categories.json"))
                     {
                         string categoriesJSON = r.ReadToEnd();
                         IList<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJSON);

                         foreach (var category in categories)
                         {
                             ctx.Categories.AddOrUpdate(category);
                         }
                     }
                 }
                 ctx.SaveChanges();

                if(ctx.Products.Count() == 0)
                {
                    using (StreamReader r = new StreamReader(@"../../Resources/products.json"))
                    {
                        string productsJSON = r.ReadToEnd();
                        var users = ctx.Users.ToArray();
                        var categories = ctx.Categories.ToArray();
                        IList<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJSON);

                        foreach(var product in products)
                        {
                            bool hasBuyer = rnd.Next(4) == 0 ? false : true;

                            var sellerID = rnd.Next(1, users.Count());
                            var seller = users[sellerID];
                            seller.ProductsSold.Add(product);

                            product.Seller = seller;

                            if (hasBuyer)
                            {
                                var buyerId = rnd.Next(1, users.Count());
                                var buyer = users[buyerId];
                                buyer.ProductsBought.Add(product);
                                product.Buyer = buyer;
                            }

                            var categoryId = rnd.Next(1, categories.Count());
                            var category = categories[categoryId];
                            product.Categories.Add(category);
                            category.Products.Add(product);

                            ctx.Products.AddOrUpdate(product);
                            ctx.SaveChanges();
                        }
                    }                   
                }
            }           
        }
    }
}
