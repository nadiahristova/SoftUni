namespace Point3D.Models
{
    public class Point3D
    {
        private double x;
        private double y;
        private double z;

        private static readonly Point3D startignPoint = new Point3D(0, 0, 0);

        public static Point3D StartignPoint
        {
            get { return Point3D.startignPoint; }
        }

        public Point3D(double x = 0, double y = 0, double z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public double Z
        {
            get { return this.z; }
            set { this.z = value; }
        }

        public override string ToString()
        {
            return string.Format("{{x = {0}, y = {1}, z = {2}}}", this.x, this.y, this.z);
        }

    }
}
