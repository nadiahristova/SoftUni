namespace Point3D
{
    using System;

    using Models;
    using Manipulators;

    class PlayWithPoints
    {
        public static void Main()
        {
            Point3D firstPoint = new Point3D();
            Point3D secondPoint = new Point3D(2.5, 55556, 8);
            firstPoint.X = 5;
            Console.WriteLine(firstPoint.ToString());

            Point3D beginning = Point3D.StartignPoint;
            Console.WriteLine("The beginning of the coordinate system is {0}", beginning.ToString());

            double distance = DistanceCalculator.BetweenTwoPointsIn3D(beginning, firstPoint);
            Console.WriteLine("The distance between them is: " + distance);
            Console.WriteLine();

            Path3D path1 = new Path3D(firstPoint, secondPoint, beginning, firstPoint);

            Console.Write("Enter path name: ");
            string pathName = Console.ReadLine();

            while (!Storage.WritePath(path1, pathName))
            {
                Console.Write("Path with that name already exists. Enter diffrent name:");
                pathName = Console.ReadLine();
            }

            Console.WriteLine("File successfully created.\n");

            Path3D path2 = new Path3D(beginning, firstPoint, secondPoint);
            Storage.WritePath(path2, "Path2");

            Console.WriteLine(Storage.ReadPath("Path2"));
            Console.WriteLine();
            Console.WriteLine(Storage.ReadPath("Path"));
        }
    }
}
