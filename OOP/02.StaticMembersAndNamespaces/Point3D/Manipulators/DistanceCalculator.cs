namespace Point3D.Manipulators
{
    using System;

    using Models;

    public static class DistanceCalculator
    {
        private const int Pow = 2;

        public static double BetweenTwoPointsIn3D(Point3D p1, Point3D p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), Pow) + Math.Pow((p2.Y - p1.Y), Pow) + Math.Pow((p2.Z - p1.Z), Pow));
        }
    }
}
