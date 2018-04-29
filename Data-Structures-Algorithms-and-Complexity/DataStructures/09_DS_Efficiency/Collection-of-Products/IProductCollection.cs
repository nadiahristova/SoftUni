using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_of_Products
{
    public interface IProductCollection
    {
        void Add(int id, string title, string supplier, decimal price);
        bool Remove(int id);
        IEnumerable<Product> FindByPriceRange(decimal startPrice, decimal endPrice);
        IEnumerable<Product> FindByTitle(string title);
        IEnumerable<Product> FindByTitleAndPrice(string title, decimal price);
        IEnumerable<Product> FindByTitleAndPriceRange(string title, decimal startPrice, decimal endPrice);
        IEnumerable<Product> FindBySupplierAndPrice(string supplier, decimal price);
        IEnumerable<Product> FindBySupplierAndPriceRange(string supplier, decimal startPrice, decimal endPrice);
    }
}
