namespace OrdersProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Models;

    public class DataMapper
    {
        private string categoriesFilePath;
        private string productsFilePath;
        private string ordersFilePath;

        public DataMapper(string categoriesFilePath, string productsFilePath, string ordersFilePath)
        {
            this.CategoriesFilePath = categoriesFilePath;
            this.ProductsFilePath = productsFilePath;
            this.OrdersFilePath = ordersFilePath;
        }

        public DataMapper()
            : this("../../Data/categories.txt", "../../Data/products.txt", "../../Data/orders.txt")
        { }

        public string CategoriesFilePath
        {
            get { return this.categoriesFilePath; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("File path is required.");
                }

                if (!File.Exists(value))
                {
                    throw new FileNotFoundException(nameof(CategoriesFilePath),"File is not found.");
                }

                this.categoriesFilePath = value;
            }
        }

        public string ProductsFilePath
        {
            get { return this.productsFilePath; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("File path is required.");
                }

                if (!File.Exists(value))
                {
                    throw new FileNotFoundException(nameof(ProductsFilePath), "File is not found.");
                }

                this.productsFilePath = value;
            }
        }

        public string OrdersFilePath
        {
            get { return this.ordersFilePath; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("File path is required.");
                }

                if (!File.Exists(value))
                {
                    throw new FileNotFoundException(nameof(OrdersFilePath), "File is not found.");
                }

                this.ordersFilePath = value;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categoryFileLines = ReadFile(this.categoriesFilePath, true);
            var categoryData = categoryFileLines
                                .Select(l => l.Split(','))
                                .Select(c => new Category
                                {
                                    Id = int.Parse(c[0]),
                                    Name = c[1],
                                    Description = c[2]
                                });

            return categoryData;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var productsFileLines = ReadFile(this.productsFilePath, true);

            var productsData = productsFileLines
                                .Select(l => l.Split(','))
                                .Select(p => new Product
                                {
                                    Id = int.Parse(p[0]),
                                    Name = p[1],
                                    CategoryId = int.Parse(p[2]),
                                    Price = decimal.Parse(p[3]),
                                    ProductsInStock = int.Parse(p[4]),
                                });

            return productsData;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var ordersFileLines = ReadFile(this.ordersFilePath, true);
            var ordersData = ordersFileLines
                                .Select(l => l.Split(','))
                                .Select(p => new Order
                                {
                                    Id = int.Parse(p[0]),
                                    ProductId = int.Parse(p[1]),
                                    Quantity = int.Parse(p[2]),
                                    Discount = decimal.Parse(p[3]),
                                });

            return ordersData;
        }

        private List<string> ReadFile(string filePath, bool hasHeader)
        {
            var allLines = new List<string>();
            using (var reader = new StreamReader(filePath))
            {
                string currentLine;
                if (hasHeader)
                {
                    reader.ReadLine();
                }

                while ((currentLine = reader.ReadLine()) != null)
                {
                    allLines.Add(currentLine);
                }
            }

            return allLines;
        }
    }
}
