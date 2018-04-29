namespace SU_Learning_System.Entities.Students
{
    using System.Text;
    
    using SU_Learning_System.Entities.Parents.Student;
    
    internal class GraduateStudent : Student
    {
        public static int NumGraduateStudents = 0;

        internal GraduateStudent(string fName, string lName, int age,
            string studentNumber, float? avgGrade) : base(fName, lName, age,
            studentNumber, avgGrade) 
        {
            GraduateStudent.NumGraduateStudents++;
        }

        public override string ToString()
        {
            StringBuilder studentInfo = new StringBuilder();

            studentInfo.AppendLine("Graduate Student: " + this.FirstName + " " + this.LastName);
            studentInfo.AppendLine("Student's number: " + this.StudentNumber);
            studentInfo.AppendLine("Age: " + this.Age);
            studentInfo.AppendLine("Average Grade: " + this.AvgGrade ?? "[no exams taken]");

            return studentInfo.ToString();
        }
    }
}
