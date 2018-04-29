using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalTree
{
    public class Interval<T> : System.Object, IComparable<Interval<T>>
        where T : IComparable
    {
        internal T minValue;
        internal T maxValue;

        public Interval(T value)
            : this(value, value)
        {
        }
        public Interval(T min, T max)
        {
            if(min.CompareTo(max) > 0)
                throw new ArgumentException("Incorrect Input. Min Value must be <= Max Value of the interval.");

            this.minValue = min;
            this.maxValue = max;
        }

        internal bool ContainsValue(T value)
        {
            return this.minValue.CompareTo(value) < 0 &&
                this.maxValue.CompareTo(value) > 0;
        }

        internal bool Intersects(Interval<T> other)
        {
            return this.minValue.CompareTo(other.maxValue) <= 0 &&
                this.maxValue.CompareTo(other.minValue) >= 0;
        }

        public int CompareTo(Interval<T> other)
        {
            return this.minValue.CompareTo(other.minValue);
        }
        public override string ToString()
        {
            return string.Format("[ {0} - {1} ]", this.minValue, this.maxValue);
        }

        public bool Equals(Interval<T> other)
        {
            // If parameter is null return false:
            if ((object)other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.minValue.Equals(other.minValue) && this.maxValue.Equals(other.maxValue);
        }
        
        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 37 + this.minValue.GetHashCode();
            hash = hash * 37 + this.maxValue.GetHashCode();
            return hash;
        }
    }
}
