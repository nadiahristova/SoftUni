namespace Point3D.Models
{
    using System.Text;

    public class Path3D
    {
        private Point3D[] path;

        public Path3D(params Point3D[] points)
        {
            this.Points = points;
        }

        public Point3D[] Points
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public override string ToString()
        {
            var pathInfo = new StringBuilder();

            pathInfo.AppendLine("Path:");
            var path = this.Points;

            for (int i = 0; i < path.Length; i++)
                pathInfo.AppendLine(i + " point: " + path[i].ToString());

            return pathInfo.ToString();
        }
    }
}
