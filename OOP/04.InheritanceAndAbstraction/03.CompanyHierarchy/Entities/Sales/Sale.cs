namespace CompanyHierarchy.Entities.Sales
{
    using System;

    using Interfaces;
    using System.Text;

    class Sale : ISale, IEquatable<Sale>
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 55;

        private DateTime date;
        private decimal price;
        private string productName;

        private static readonly DateTime minPurchaseDate = new DateTime(2005, 1, 1, 0, 0, 0);

        public Sale(string productName, decimal price, DateTime date)
        {
            this.ProductName = productName;
            this.Price = price;
            this.Date = date;
        }

        public Sale(string productName, decimal price) : this(productName, price, DateTime.Now)
        {  }

        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if (value.CompareTo(minPurchaseDate) < 0)
                    throw new ArgumentOutOfRangeException($"Sale occured earlier than {minPurchaseDate.ToString("dd.MM, yyyy")} is considered outdated.");

                this.date = value;
            }
        }

        public decimal Price
        {
            get { return this.price; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Product's price cannot be negative.");

                    this.price = value;
                }
        }

        public string ProductName
        {
            get { return this.productName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Product name is required.");

                if (value.Length < MinNameLength || value.Length > MaxNameLength)
                    throw new ArgumentOutOfRangeException($"Product's name length shpuld be in the boundaries: [{MinNameLength}, {MaxNameLength}]");

                this.productName = value;
            }
        }

        public override string ToString()
        {
            var saleInfo = new StringBuilder();

            saleInfo.AppendLine($"Sale occurred on: {this.Date.ToString("dd.MMM, yyyy hh:mm:ss")}");
            saleInfo.AppendLine($"Product name: {this.ProductName}, price: {string.Format("{0:f2} lv", this.Price)}");

            return saleInfo.ToString();
        }

        public bool Equals(Sale other)// we need this because we use HashSet
        {
            return this.ProductName.Trim() == other.ProductName.Trim();
        }
    }
}
