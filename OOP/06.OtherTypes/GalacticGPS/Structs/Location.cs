namespace GalacticGPS.Structs
{
    using Enums;

    using System;
    using System.Reflection;

    struct Location
    {
        private const double MinLatitude = -90;
        private const double MaxLatitude = 90;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        private double latitude;
        private double longitude;

        public Location(double latitude, double longitude, Planet planet) : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Planet = planet;
        }

        public double Latitude
        {
            get { return this.latitude; }
            set
            {
                if (MinLatitude > value || MaxLatitude < value)
                    throw new ArgumentOutOfRangeException(MethodBase.GetCurrentMethod().Name.Substring(4), 
                        $"Latitude is in range [{MinLatitude}, {MaxLatitude}]");

                this.latitude = value;
            }
        }
        

        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (MinLongitude > value || MaxLongitude < value)
                    throw new ArgumentOutOfRangeException(nameof(Longitude),
                        $"Longitude is in range [{MinLongitude}, {MaxLongitude}]");

                this.longitude = value;
            }
        }

        public Planet Planet { get; set; }

        public override string ToString()
            => $"{this.Latitude} {this.Longitude} - {this.Planet}";
    }
}
