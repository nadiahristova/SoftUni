namespace Shapes.Entities
{
    using System;
    using Interfaces;

    class Circle : IShape
    {
        private double radius = 0;

        public Circle() { }

        public Circle(double radius)
        {
            this.Raduis = radius;
        }

        public double Raduis
        {
            get { return this.radius; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("radius", $"Cannot be negative.");

                this.radius = value;
            }
        }

        public double CalculateArea()
        {
            return Math.PI * Math.Pow(this.Raduis, 2);
        }

        public double CalculatePerimeter()
        {
            return 2 * Math.PI * this.Raduis;
        }
    }
}
