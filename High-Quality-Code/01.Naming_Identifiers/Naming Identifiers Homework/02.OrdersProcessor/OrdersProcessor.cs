namespace OrdersProcessor
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Globalization;

    class OrdersProcessor
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var db = new DataMapper();
            var categories = db.GetAllCategories();
            var products = db.GetAllProducts();
            var orders = db.GetAllOrders();

            // Names of the 5 most expensive products
            var top5MostExpensiveProducts = products
                .OrderByDescending(p => p.Price)
                .Take(5)
                .Select(p => p.Name);
            Console.WriteLine(string.Join(Environment.NewLine, top5MostExpensiveProducts));

            Console.WriteLine(new string('-', 10));

            // Number of products in each category
            var productsCountPerCategory = products
                .GroupBy(p => p.CategoryId)
                .Select(grp => new { Category = categories.First(c => c.Id == grp.Key).Name, Count = grp.Count() })
                .ToList();
            foreach (var product in productsCountPerCategory)
            {
                Console.WriteLine("{0}: {1}", product.Category, product.Count);
            }

            Console.WriteLine(new string('-', 10));

            // The 5 top products (by order quantity)
            var top5MostPurchasedProducts = orders
                .GroupBy(o => o.ProductId)
                .Select(grp => new {Product = products.First(p => p.Id == grp.Key).Name, Quantities = grp.Sum(p => p.Quantity)})
                .OrderByDescending(p => p.Quantities)
                .Take(5);
            foreach (var product in top5MostPurchasedProducts)
            {
                Console.WriteLine("{0}: {1}", product.Product, product.Quantities);
            }

            Console.WriteLine(new string('-', 10));

            // The most profitable category
            var mostProfitableCategory = orders
                .GroupBy(o => o.ProductId)
                .Select(grp => new { CategoryId = products.First(p => p.Id == grp.Key).CategoryId, Price = products.First(p => p.Id == grp.Key).Price, Quantity = grp.Sum(p => p.Quantity) })
                .GroupBy(p => p.CategoryId)
                .Select(grp => new { CategoryName = categories.First(c => c.Id == grp.Key).Name, TotalQuantity = grp.Sum(p => p.Quantity * p.Price) })
                .OrderByDescending(c => c.TotalQuantity)
                .First();
            Console.WriteLine("{0}: {1}", mostProfitableCategory.CategoryName, mostProfitableCategory.TotalQuantity);
        }
    }
}
