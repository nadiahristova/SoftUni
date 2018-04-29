using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_of_Products
{
    public class Product : IComparable<Product>, IEquatable<Product>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public bool Equals(Product other)
        {
            return this.Id == other.Id;
        }
        public override string ToString()
        {
            return "ID: " + this.Id + " " + this.Title + ", Supplier: " + this.Supplier + ", Price: " + this.Price + "lv";
        }
    }
}
