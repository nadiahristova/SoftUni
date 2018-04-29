using System;
using System.IO;
using System.Linq;
using Wintellect.PowerCollections;

class ProductsInPriceRange
{
    private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static string pathToDir = @"..\..\Report";
    static void Main()
    {
        OrderedBag<Product> sorter = new OrderedBag<Product>();
        var random = new Random();

        for (int i = 0; i < 500000; i++)
        {
            int lengthOfProductName = random.Next(5, 15);
            var productName = new string(
                Enumerable.Repeat(chars, lengthOfProductName)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            decimal price = (decimal)random.Next(25, 1000);
            sorter.Add(new Product() { Name = productName + " Product", Price = price});
        }

        Directory.CreateDirectory(pathToDir);

        using (StreamWriter s = new StreamWriter(pathToDir + @"\10_000_PriceRangeReports.txt"))
        {
            for(int i = 0; i< 10000; i++)
            {
            
                    decimal minPrice = random.Next(0, 999);
                    decimal maxPrice = random.Next((int)minPrice, 1000);

                    var products = sorter.Range(new Product() { Price = minPrice }, true, new Product() { Price = maxPrice }, true);

                    s.WriteLine("--- {2}) First 20 Products in Price Range [{0:f2}lv; {1:f2}lv] ---", minPrice, maxPrice, i);
                    for (int j = 0; j < 20 && j < products.Count; j++)
                    {
                        s.WriteLine("{0:f2}lv - {1}", products[j].Price, products[j].Name);
                    }
                    
                    s.WriteLine();
             }
        }
    }
}

