namespace PC_Catalog
{
    using System;
    using System.Linq;    
    using System.Text;
    using System.Collections.Generic;

    public class Computer
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set
                {
                    if(string.IsNullOrEmpty(value))
                        throw new ArgumentException("Invalid Computer Name Entry");

                    this.name = value;
                }

        }

        public decimal Price
        {
            get { return CalcComputerPrice(); } 
        }

        public List<Component> Components { get; set; }

        private decimal CalcComputerPrice()
        {            
            decimal? sum = this.Components.Sum(c => c.Price);
            return sum.HasValue ? sum.Value : 0m;
        }

        public Computer(string name, List<Component> components)
        {
            this.Name = name;
            this.Components = components;            
        }

        public Computer(string name) : this(name, new List<Component>())
        { }

        public override string ToString()
        {
            StringBuilder computerData = new StringBuilder();

            computerData.AppendLine("Computer name: " + this.name);
            computerData.AppendLine("Component(s):");
            this.Components.ForEach(c => Console.WriteLine(c));
            computerData.AppendLine(new String('-', 25));
            computerData.AppendLine("Overall computer price: " + string.Format("{0:0.00} lv.", this.Price));

            return computerData.ToString();
        }
    }
}
