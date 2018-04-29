namespace SU_Learning_System.Entities.Students
{
    using System;
    using System.Text;

    using SU_Learning_System.Interfaces;
    using SU_Learning_System.Entities.Parents.Student;    
    
    internal class OnsiteStudent : CurrentStudent, IOnsiteStudent
    {
        private int numberVisits;

        public static int NumOnsiteStudents = 0;
        public int NumberVisits
        {
            get { return this.numberVisits; }
            set
                {
                    if(value < 0 || value > 256 )
                        throw new ArgumentException("Unvalid number of visits.");

                    this.numberVisits = value;
                }
        }

        internal OnsiteStudent(string fName, string lName, int age, 
            string studentNumber, float? avgGrade, string currentCourse, int numberVisits = 0)
            :base(fName, lName, age, studentNumber, avgGrade, currentCourse)
        {
            this.NumberVisits = numberVisits;

            OnsiteStudent.NumOnsiteStudents++;
        }

        public override string ToString()
        {
            StringBuilder studentInfo = new StringBuilder();

            studentInfo.AppendLine("Onsite Student: " + this.FirstName + " " + this.LastName);
            studentInfo.AppendLine("Student's number: " + this.StudentNumber);
            studentInfo.AppendLine("Age: " + this.Age);
            studentInfo.AppendLine("Average Grade: " + this.AvgGrade ?? "[no exams taken]");
            studentInfo.AppendLine("Attending: " + this.CurrentCourse);

            return studentInfo.ToString();
        }
    }
}
