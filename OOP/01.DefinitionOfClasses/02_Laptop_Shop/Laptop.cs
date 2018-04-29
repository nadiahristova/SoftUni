namespace Laptop_Shop
{
    using System;
    using System.Text;
    
    class Laptop
    {
        private string model;
        private string manufacturer;
        private string processor;
        private string ram;
        private string graphicsCard;
        private string hdd;
        private string screen;
        private Battery battery;
        private decimal price;

        public Laptop(string model, decimal price, string manufacturer = null, string processor = null, string ram = null, 
            string graphicsCard = null, string hdd = null, string screen = null, Battery battery = null) : this(model, price)
        {                   
            this.Manufacturer = manufacturer;
            this.Processor = processor;
            this.RAM = ram;
            this.GraphicsCard = graphicsCard;
            this.HDD = hdd;
            this.Screen = screen;
            this.Battery = battery;                        
        }

        public Laptop(string model, decimal price)
        {
            this.Model = model;
            this.Price = price;
        }

        public string Model
        {
            get { return this.model; }
            set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Invalid entry for Laptop Model.");

                    this.model = value;
                }
        }

        public string Manufacturer
        {
            get { return this.manufacturer; }
            set
                {
                    if (value != null && value.Trim() == string.Empty)
                        throw new ArgumentException("Invalid entry for Laptop Manufacturer.");

                    this.manufacturer = value;
                }
        }

        public string Processor
        {
            get { return this.processor; }
            set
                {
                    if (value != null && value.Trim() == string.Empty)
                        throw new ArgumentException("Invalid entry for Laptop Processor.");

                    this.processor = value;
                }
        }

        public string RAM
        {
            get { return this.ram; }
            set
                {
                    if (value.Trim() == string.Empty)
                        throw new ArgumentException("Invalid entry for Laptop RAM.");

                    this.ram = value;
                }
        }

        public string GraphicsCard
        {
            get { return this.graphicsCard; }
            set
                {
                    if (value != null && value.Trim() == string.Empty)
                    {
                        throw new ArgumentException("Invalid entry for Laptop Graphics Card.");
                    }
                    this.graphicsCard = value;
                }
        }

        public string HDD
        {
            get { return this.hdd; }
            set
                {
                    if (value != null && value.Trim() == string.Empty)
                        throw new ArgumentException("Invalid entry for Laptop HDD.");

                    this.hdd = value;
                }
        }

        public string Screen
        {
            get { return this.screen; }
            set
                {
                    if (value != null && value.Trim() == string.Empty)
                        throw new ArgumentException("Invalid entry for Laptop screen.");

                    this.screen = value;
                }
        }

        public Battery Battery
        {
            get { return this.battery; }
            set { this.battery = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set
                {
                    if(value<0)
                        throw new ArgumentException("Invalid Laptop price entry");

                    this.price = value;
                }
        }

        public override string ToString()
        {
            StringBuilder laptopDescription = new StringBuilder();
            laptopDescription.AppendLine("model : " + this.model);

            if(this.Manufacturer != null)
                laptopDescription.AppendLine("manufacturer : " + this.manufacturer);

            if(this.Processor != null)
                laptopDescription.AppendLine("processor : " + this.processor);

            if(this.RAM != null)
                laptopDescription.AppendLine("RAM : " + this.ram);

            if (this.GraphicsCard != null)
                laptopDescription.AppendLine("Graphics Card : " + this.graphicsCard);

            if (this.HDD != null)
                laptopDescription.AppendLine("HDD : " + this.hdd);

            if (this.Screen != null)
                laptopDescription.AppendLine("screen : " + this.screen);

            if (this.battery != null)
                laptopDescription.Append(this.battery.ToString());
            
            laptopDescription.AppendLine("price : " + String.Format("{0:0.00} lv.", this.price));

            return laptopDescription.ToString();
        }
    }
}
