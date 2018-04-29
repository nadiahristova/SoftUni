namespace Laptop_Shop
{
    using System;
    using System.Text;
    using System.Globalization;

    class Battery
    {
        private string type;
        private float life;
        
        public string Type
        {
            get { return this.type; }
            set
                {
                    if (string.IsNullOrEmpty(value.Trim()))
                        throw new ArgumentException("Infornation about the battery's type is required.");

                    this.type = value;
                }
        }

        public float Life
        {
            get { return this.life; }
            set
                {
                    if(value<0){
                        throw new ArgumentException("Invalid battery life entry.");
                    }

                    this.life = value;
                }
        }

        public Battery(string type, float life = 0.0f)
        {
            this.Type = type;
            this.Life = life;
        }

        public override string ToString()
        {
            StringBuilder printBattery = new StringBuilder();
            printBattery.AppendLine("Battery Model: " + this.type);
            printBattery.AppendLine("Battery Life: " + 
                String.Format(CultureInfo.InvariantCulture, "{0:0.0}", this.life) + " hours");

            return printBattery.ToString();
        }
    }
}
