namespace PC_Catalog
{
    using System;

    public class Component
    {
        private string name;
        private string details;
        private decimal price;

        private const int MaxLengthDescription = 345;

        public Component(string name, decimal price, string details = null)
        {
            this.Name = name;
            this.Price = price;
            this.Details = details;
        }

        public string Name
        {
            get { return this.name; }
            set
                {   
                    if(string.IsNullOrEmpty(value))
                        throw new ArgumentException("Component name is expected.");

                    this.name = value;
                }
        }

        public decimal Price
        {
            get { return this.price; }
            set
                {
                    if(value < 0m)
                        throw new ArgumentException("Price should be greater than 0.");

                    this.price = value;
                }
        }

        public string Details
        {
            get { return this.details; }
            set
                {
                    if(value != null && value.Length > MaxLengthDescription)
                        throw new ArgumentException("Details provided should be max " + MaxLengthDescription + " characters long.");

                    this.details = value;
                }
        }

        public override string ToString()
        {
            string componentInfo = string.Format("{0} : {1:0.00} lv", this.name, this.price);
            if (this.details != null)
                componentInfo += " (" + this.details + ")";

            return componentInfo;
        }
    }    
    
}
