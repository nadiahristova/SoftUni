namespace Shapes.Entities.Proginitor
{
    using System;
    using Interfaces;

    abstract class BasicShape : IShape
    {
        private double width;
        private double height;

        protected BasicShape(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public double Width
        {
            get { return this.width; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException($"Width of {this.GetType().Name} cannot be negative.");

                    this.width = value;
                }
        }

        /// <summary>
        /// The hight of the figure.</summary>
        /// <value>
        /// Cannot be negative number. </value>
        public double Height
        {
            get { return this.height; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException($"Hight of {this.GetType().Name} cannot be negative.");

                this.height = value;
            }
        }

        public abstract double CalculateArea();

        public abstract double CalculatePerimeter();
    }
}
