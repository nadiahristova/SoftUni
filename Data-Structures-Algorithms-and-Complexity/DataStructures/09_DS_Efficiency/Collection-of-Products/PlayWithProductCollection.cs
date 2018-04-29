namespace Collection_of_Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class PlayWithProductCollection
    {
        static void Main()
        {
            var products = new ProductCollection();

            products.Add(1, "Product1", "Pesho", 28.9m);
            Console.WriteLine("Added a product. Count = " + products.Count);

            products.Add(2, "Product2", "Pesho", 28.9m);
            Console.WriteLine("Duplicated product. Count = " + products.Count);

            products.Add(2, "Product2", "Pesho", 18.99m);
            Console.WriteLine("Added a product. Count = " + products.Count);

            products.Add(3, "Product3", "Asen", 18.99m);
            Console.WriteLine("Added a product. Count = " + products.Count);
            
            products.Add(4, "Product2", "Asen", 2m);
            Console.WriteLine("Added a product. Count = " + products.Count);

            var productWithGivenId = products.FindProduct(3);
            Console.WriteLine("Product with id 3 is {0}", productWithGivenId.ToString());

            var personsWithGivenTitle = products.FindByTitle("Product2");
            Console.WriteLine("Products with titles 'Product2':\n{0}",
                string.Join(", \n", personsWithGivenTitle.Select(p => p.ToString())));

            products.Remove(2);

            personsWithGivenTitle = products.FindByTitle("Product2");
            Console.WriteLine("Products with titles 'Product2':\n{0}",
                string.Join(", \n", personsWithGivenTitle.Select(p => p.ToString())));

            var personsWithTitleAndPrice = products.FindByTitleAndPrice("Product2", 18.99m);
            Console.WriteLine("Products with title 'Product2' and price 18.99lv:\n{0}",
                string.Join(", \n", personsWithTitleAndPrice.Select(p => p.ToString())));

            var personsWithTitleAndPriceRange = products.FindByTitleAndPriceRange("Product2", 0, 18.99m);
            Console.WriteLine("Products with title 'Product2' and price between 0 and 18.99lv:\n{0}",
                string.Join(", \n", personsWithTitleAndPriceRange.Select(p => p.ToString())));

            var personsWithSupplierAndPrice = products.FindBySupplierAndPrice("Asen", 3);
            Console.WriteLine("Products with supplier 'Asen' and price 3lv:\n{0}",
                string.Join(", \n", personsWithSupplierAndPrice.Select(p => p.ToString())));

            var personsWithSupplierAndPriceRange = products.FindBySupplierAndPriceRange("Asen", 1, 20.9m);
            Console.WriteLine("Products with supplier 'Asen' and price between 0 and 20.9lv':\n{0}",
                string.Join(", \n", personsWithSupplierAndPriceRange.Select(p => p.ToString())));
        }
    }
}
