namespace Shapes
{
    using Entities;
    using Interfaces;
    using Entities.Proginitor;    

    using System;
    using System.Collections.Generic;

    class PlayWithShapes
    {
        static void Main(string[] args)
        {
            List<IShape> shapes = new List<IShape>()
            {
                new Circle() { Raduis = 5 },
                new Circle(8.3)
            };

            IEnumerable<IShape> polygons = new List<BasicShape>()
            {
                new Rectangle(5, 6),
                new Rhombus(7, 8.9)
            };

            shapes.AddRange(polygons);

            foreach(var shape in shapes)
            {
                Type shapeType = shape.GetType();
                if (shape.GetType() == typeof(Circle))
                {
                    Console.WriteLine($"The area of a {shape.GetType().Name} with radius {(shape as Circle).Raduis} "+ 
                        $"is: {string.Format("{0:0.00}", shape.CalculateArea())}");
                    Console.WriteLine($"It's perimeter is: {string.Format("{0:0.00}", shape.CalculatePerimeter())}");
                }
                else
                {
                    Console.WriteLine($"The area of {shape.GetType().Name} with side {((BasicShape)shape).Width} and height "+ 
                        $"{((BasicShape)shape).Width} is {shape.CalculateArea()}");
                    Console.WriteLine($"It's perimeter is: {shape.CalculatePerimeter()}");
                }
            }
        }
    }
}
