namespace Shapes.Entities
{
    using Proginitor;

    class Rhombus : BasicShape
    {

        /// <summary>
        /// Rhombus constructor.</summary>
        /// <param name="width"> The side of the rhombus
        /// <value>Cannot be negative.</value></param>
        /// <param name="height"> The altitude of the rhombus
        /// <value>Cannot be negative.</value></param>
        public Rhombus(double width, double height) : base(width, height)
        { }

        public override double CalculateArea()
        {
            return this.Width * this.Height;
        }

        public override double CalculatePerimeter()
        {
            return 4 * this.Width;
        }
    }
}
