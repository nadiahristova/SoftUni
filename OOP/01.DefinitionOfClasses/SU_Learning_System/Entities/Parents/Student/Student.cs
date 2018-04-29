namespace SU_Learning_System.Entities.Parents.Student
{
    using System;    

    using SU_Learning_System.Interfaces;
    using SU_Learning_System.Entities.Parents.Progenitor; 
       
    internal abstract class Student : Person, IStudent
    {
        private float? avgGrade;
        private string studentNumber;        

        public static int NumStudents;

        protected Student(string fName, string lName, int age, string studentNumber, 
            float? avgGrade = null):base(fName, lName, age)
        {
            this.StudentNumber = studentNumber;
            this.AvgGrade = avgGrade;

            NumStudents++;
        }

        public string StudentNumber 
        {
            get { return this.studentNumber; }
            set 
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new NullReferenceException("A studen must have an id number.");
                    } 
                
                    this.studentNumber = value; 
                }
        }

        public float? AvgGrade
        {
            get { return this.avgGrade; }
            set 
                {
                    if (value != null && (value.Value < 2 || value.Value > 6))
                        throw new ArgumentOutOfRangeException("Average grade must be in range [2, 6]");

                    this.avgGrade = value; 
                }
        }
    }
}
