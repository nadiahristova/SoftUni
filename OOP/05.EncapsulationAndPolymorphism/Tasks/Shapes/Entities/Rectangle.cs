namespace Shapes.Entities
{
    using Proginitor;

    class Rectangle : BasicShape
    {
        /// <summary>
        /// Rectangle constructor.</summary>
        public Rectangle(double width, double height) : base(width, height)
        {  }

        public override double CalculateArea()
        {
            return this.Width * this.Height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (this.Width + this.Height);
        }
    }
}
