namespace HTMLDispatcher.HTMLBuilders
{
    using System;
    using System.Collections.Generic;

    public class ElementBuilder
    {
        private string elementName;
        private List<Models.Attribute> attributes = new List<Models.Attribute>();
        private string content = string.Empty;
                    
        public ElementBuilder(string name)
        {
            this.ElementName = name;
        }

        public string ElementName
        {
            get { return this.elementName; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Element's name", "The element's name cannot be null or empty.");

                this.elementName = value;
            }
        }

        public void AddAttribute(string name, string value)
        {
            var currAtt = new Models.Attribute(name, value);
            this.attributes.Add(currAtt);
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public void AddContent(string content)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            List<string> attributes = new List<string>();
            this.attributes.ForEach(att => attributes.Add(att.ToString()));

            string line = "<" + this.elementName + " " + string.Join(" ", attributes) + ">" 
                + this.content + "</" + this.elementName + ">";

            return line;
        }

        public static string operator * (ElementBuilder element, int multiplier)
        {
            string multipliedElements = null;

            for (int i = 0; i < multiplier; i++)
                multipliedElements += element.ToString();

            return multipliedElements;
        }
    }
}
