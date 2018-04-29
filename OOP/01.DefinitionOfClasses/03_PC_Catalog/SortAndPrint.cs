namespace PC_Catalog
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Globalization;
    using System.Collections.Generic;    
    
    class SortAndPrint
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Console.OutputEncoding = Encoding.UTF8;

            Component processor1 = new Component("INTEL 5.5", 15341.4m, "4 GHz, 8 core");
            Component processor2 = new Component("INTEL 6", 56.4m);
            Component graphCard1 = new Component("NVIDIA 567HG", 646.4m);
            Component graphCard2 = new Component("NVIDIA 67456HG", 6846.4m, "bad @$$ video card");
            Component graphCard3 = new Component("NVIDIA 100HG", 6846.4m, "@ video card");
            Component motherboard1 = new Component("Nfsfsf", 246.4m);
            Component motherboard2 = new Component("Nfsfsadasdsf", 346.4m);

            Computer comp1 = new Computer("PC1", new List<Component> { processor1, graphCard1, motherboard2 });
            Computer comp2 = new Computer("PC2", new List<Component> { processor1, graphCard2, motherboard1 });
            Computer comp3 = new Computer("PC3", new List<Component> { processor2, graphCard3, motherboard2 });

            Computer[] computers = new Computer[]{comp3, comp2, comp1};

            Array.Sort(computers, (a, b) => a.Price.CompareTo(b.Price));

            computers.ToList().ForEach(Console.WriteLine);
        }
    }
}
