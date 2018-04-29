using System;
using System.Collections.Generic;
using System.Linq;

internal class Product : IComparable<Product>
{
    public decimal Price { get; set; }
    public string Name { get; set; }

    public int CompareTo(Product other)
    {
        return this.Price.CompareTo(other.Price);
    }
}

