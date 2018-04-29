namespace BookShopSystem.Data.Migrations
{
    using BookShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;

    internal class Configuration : DbMigrationsConfiguration<BookShopEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookShopSystem.Data.BookShopEntities context)
        {
            var ctx = new BookShopEntities();
            var random = new Random();

            if(!ctx.Categories.Any())
                using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"Resources/categories.txt"))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        var data = line.Split(new[] { ' ' }, 1);
                        var name = data[0];

                        ctx.Categories.Add(new Category()
                        {
                            Name = name
                        });
                        line = reader.ReadLine();
                    }
                }
            ctx.SaveChanges();

            if (!ctx.Authors.Any())
                using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"Resources/authors.txt"))
                {
                    string line = reader.ReadLine();
                    line = reader.ReadLine();
                    var authors = ctx.Authors.ToArray();

                    while (line != null)
                    {                    
                        var data = line.Split(new[] { ' ' }, 2);
                        var fName = data[0];
                        var lName = data[1];

                        ctx.Authors.Add(new Author()
                        {
                            FirstName = fName,
                            LastName = lName
                        });

                        line = reader.ReadLine();
                    }
                }
            ctx.SaveChanges();

            if (!ctx.Books.Any())
                using(var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"Resources/books.txt"))
                {
                    string line = reader.ReadLine();
                    line = reader.ReadLine();
                    var authors = ctx.Authors.ToArray();

                    while(line != null)
                    {
                        var data = line.Split(new[] { ' ' }, 6);
                        int authorIndex = random.Next(1, authors.Length);
                        var author = authors[authorIndex-1];
                        var edition = (EditionType)int.Parse(data[0]);
                        var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                        var copies = int.Parse(data[2]);
                        var price = decimal.Parse(data[3], CultureInfo.InvariantCulture);
                        var ageRestriction = (AgeRestriction)int.Parse(data[4]);
                        var title = data[5];
                        var randomCatId = random.Next(1, ctx.Categories.Count());
                        var category = ctx.Categories.Find(randomCatId);

                        var newBook = new Book()
                            {
                                Author = author,
                                EditionType = edition,
                                ReleaseDate = releaseDate,
                                Copies = copies,
                                Price = price,
                                AgeRestriction = ageRestriction,
                                Title = title,
                                Categories = new HashSet<Category> { category }
                            };

                        newBook.Categories.Add(category);
                        category.Books.Add(newBook);

                        ctx.Books.Add(newBook);

                        line = reader.ReadLine();
                    }
                }

            ctx.SaveChanges();
         
        }   
    }
}
