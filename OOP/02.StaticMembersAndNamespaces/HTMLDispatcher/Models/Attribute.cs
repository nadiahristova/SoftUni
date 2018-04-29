namespace HTMLDispatcher.Models
{
    using System;

    class Attribute
    {
        private string name;
        private string value;

        public Attribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Attribute's name", "The attribute name cannot be null or empty.");

                this.name = value;
            }
        }

        public string Value
        {
            get { return this.value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Value of arguments", "The attribute value cannot be null or empty.");
            
                this.value = value;
            }
        }

        public override string ToString()
        {
            return this.name + "=\"" + this.value + "\"";
        }
    }
}
