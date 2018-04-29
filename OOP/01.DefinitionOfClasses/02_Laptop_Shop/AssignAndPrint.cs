namespace Laptop_Shop
{
    using System;
    using System.Collections.Generic;

    class AssignAndPrint
    {
        static void Main()
        {
            Battery bat1 = new Battery("Li-Ion, 8-cells, 2550 mAh");
            Battery bat2 = new Battery("Li-Ion", 4.5f);

            ICollection<Laptop> laptops = new List<Laptop>() 
            {
                new Laptop("Lenovo Yoga 2 Pro", 869.88m, "Lenovo", "Intel Core i5-4210U (2-core, 1.70 - 2.70 GHz, 3MB cache)",
                "8 GB", "128GB SSD", "Intel HD Graphics 4400", "13.3\" (33.78 cm) – 3200 x 1800 (QHD+), IPS sensor display", bat1),
                new Laptop("Aspire E3-111-C5GL", 259.49m),
                new Laptop("Acer some model", 1567.4366m, battery : bat2, processor : "3.2 GHz", ram : "16 GB")
            };

            foreach(var laptop in laptops)
            {
                Console.WriteLine(laptop);
            }
        }
    }
}
