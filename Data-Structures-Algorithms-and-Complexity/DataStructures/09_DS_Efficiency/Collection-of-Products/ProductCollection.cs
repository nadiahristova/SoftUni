namespace Collection_of_Products
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;
    public class ProductCollection : IProductCollection
    {
        private Dictionary<int, Product> productsById = new Dictionary<int, Product>();
        private Dictionary<string, SortedSet<Product>> productsByTitle =
            new Dictionary<string, SortedSet<Product>>();
        private OrderedMultiDictionary<decimal, Product> productsByPriceRange = 
            new OrderedMultiDictionary<decimal, Product>(true);
        private Dictionary<string, OrderedMultiDictionary<decimal, Product>> productsByTitleAndPrice =
            new Dictionary<string, OrderedMultiDictionary<decimal, Product>>();
        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySupplierAndPrice =
            new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();


        public int Count
        {
            get { return this.productsById.Count; }
        }

        public void Add(int id, string title, string supplier, decimal price)
        {
            var product = new Product()
            {
                Id = id,
                Title = title,
                Supplier = supplier,
                Price = price
            };

            this.productsById.EnsureKeyExists(product.Id);
            this.productsById[product.Id] = product;
            this.productsByTitle.AppendValueToKey(product.Title, product);
            this.productsByPriceRange.Add(product.Price, product);
            this.productsByTitleAndPrice.AppendValueToInnerMultiDictionary(product.Title, product.Price, product);
            this.productsBySupplierAndPrice.EnsureKeyExists(product.Supplier);
            this.productsBySupplierAndPrice[product.Supplier].AppendValueToKey(product.Price, product);
        }
        public bool Remove(int id)
        {
            Product product = this.FindProduct(id);
            if(product == null)
                return false;

            this.productsById.Remove(id);
            this.productsByTitle[product.Title].Remove(product);
            this.productsByPriceRange.Remove(product.Price, product);
            this.productsByTitleAndPrice[product.Title][product.Price].Remove(product);
            this.productsBySupplierAndPrice[product.Supplier][product.Price].Remove(product);

            return true;
        }
        public Product FindProduct(int id)
        {
            Product product;
            productsById.TryGetValue(id, out product);

            return product;
        }
        public IEnumerable<Product> FindByPriceRange(decimal startPrice, decimal endPrice)
        {
            var productsINPriceRange = this.productsByPriceRange.Range(startPrice, true, endPrice, true);

            foreach(var productsByPrice in productsByPriceRange)
            {
                foreach(var product in productsByPrice.Value)
                {
                    yield return product;
                }
            }
        }
        public IEnumerable<Product> FindByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                yield break; //Returns an empty sequence of products
            }

            var productsWithGivenTitle = this.productsByTitle[title];

            foreach (var product in productsWithGivenTitle)
            {
                yield return product;
            }
        }

        public IEnumerable<Product> FindByTitleAndPrice(string title, decimal price)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                yield break;
            }

            if (!this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                yield break;
            }

            var productsByTitleAndPrice = this.productsByTitleAndPrice[title][price];

            foreach (var product in productsByTitleAndPrice)
            {
                yield return product;
            }
        }

        public IEnumerable<Product> FindByTitleAndPriceRange(string title, decimal startPrice, decimal endPrice)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                yield break;
            }

            var productsByTitleAndPriceRange = this.productsByTitleAndPrice[title].Range(startPrice, true, endPrice, true);

            if (productsByTitleAndPriceRange.Count == 0)
                yield break; 

            foreach (var productByPrice in productsByTitleAndPriceRange)
                foreach (var product in productByPrice.Value)
                    yield return product;
        }

        public IEnumerable<Product> FindBySupplierAndPrice(string supplier, decimal price)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                yield break;
            }

            if (!this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                yield break;
            }

            var productsBySupplierAndPrice = this.productsBySupplierAndPrice[supplier][price];

            foreach (var product in productsBySupplierAndPrice)
            {
                yield return product;
            }
        }

        public IEnumerable<Product> FindBySupplierAndPriceRange(string supplier, decimal startPrice, decimal endPrice)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                yield break;
            }

            var productsBySupplierAndPriceRange = this.productsBySupplierAndPrice[supplier].Range(startPrice, true, endPrice, true);

            if (productsBySupplierAndPriceRange.Count == 0)
                yield break;

            foreach (var productByPrice in productsBySupplierAndPriceRange)
                foreach (var product in productByPrice.Value)
                    yield return product;
        }

    }
}
