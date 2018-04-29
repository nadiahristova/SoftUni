namespace CustomLINQExtension.Entities
{
    using System;

    public class Student
    {
        public event PropertyChangeEventHandler<Student> PropertyChange;

        private string name;
        private int grade;

        public Student(string name, int grade)
        {
            this.Grade = grade;
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentNullException("Name", "Name is required.");

                    if (this.PropertyChange != null)
                        this.PropertyChange(this, new PropertyChangeEventArgs(nameof(this.Name), this.name, value));

                    this.name = value;
                }
        }

        public int Grade
        {
            get { return this.grade; }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException("Grade", "[0, 10]");

                if (this.PropertyChange != null)
                    this.PropertyChange(this, new PropertyChangeEventArgs(nameof(this.Grade), this.grade, value));

                this.grade = value;
            }
        }
    }
}
