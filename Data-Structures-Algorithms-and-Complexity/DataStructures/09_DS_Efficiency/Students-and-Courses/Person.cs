namespace Students_and_Courses
{
    using System;
    class Person : IComparable<Person>, IEquatable<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompareTo(Person other)
        {
            if (this.LastName.CompareTo(other.LastName) == 0)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }

            return this.LastName.CompareTo(other.LastName);
        }

        public bool Equals(Person other)
        {
            return this.FirstName == other.FirstName && this.LastName == other.LastName;
        }
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
