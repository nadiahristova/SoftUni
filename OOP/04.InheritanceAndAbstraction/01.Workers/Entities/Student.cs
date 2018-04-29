namespace Workers.Entities
{
    using System;
    using System.Text.RegularExpressions;

    using Workers.Entities.Progenitor;

    class Student : Human, IEquatable<Student>
    {
        private const int FacultyMinLength = 5;
        private const int FacultyMaxLength = 10;

        private static readonly Regex facultyNumValidationRegex = new Regex("^([a-zA-Z0-9]{5,10})$");

        private string facultyNumber;

        public Student(string firstName, string lastName, string facultyNumber)
            : base(firstName, lastName)
        {
            this.FacultyNumber = facultyNumber;
        }

        public string FacultyNumber
        {
            get { return this.facultyNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Faculty number is required.");

                value = value.Trim();

                if (!facultyNumValidationRegex.IsMatch(value))
                    throw new ArgumentException(string.Format("Faculty number consists only latin letters and digits. It's lenght must be no more than {1} letters and no less than {0} letters long.",
                        FacultyMinLength, FacultyMaxLength));

                this.facultyNumber = value;
            }
        }

        public bool Equals(Student other)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals(this.FacultyNumber, other.FacultyNumber);
        }

        public override string ToString()
        {
            return string.Format("Student: {0}\n Faculty number: {1:f2} lv.", base.ToString(), this.FacultyNumber);
        }
    }
}
