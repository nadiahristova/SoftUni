namespace SU_Learning_System.Entities.Students
{
    using System.Text;

    using SU_Learning_System.Interfaces;
    using SU_Learning_System.Entities.Parents.Student;    

    internal class OnlineStudent : CurrentStudent, ICurrentStudent
    {
        public static int NumOnlineStudents = 0;
        internal OnlineStudent(string fName, string lName, int age, 
            string studentNumber, float? avgGrade, string currentCourse)
            :base(fName, lName, age, studentNumber, avgGrade, currentCourse)
        {
            OnlineStudent.NumOnlineStudents++;
        }

        public override string ToString()
        {
            StringBuilder studentInfo = new StringBuilder();

            studentInfo.AppendLine("Online Student: " + this.FirstName + " " + this.LastName);
            studentInfo.AppendLine("Student's number: " + this.StudentNumber);
            studentInfo.AppendLine("Age: " + this.Age);
            studentInfo.AppendLine("Average Grade: " + this.AvgGrade ?? "[no exams taken]");
            studentInfo.AppendLine("Attending: " + this.CurrentCourse);

            return studentInfo.ToString();
        }
    }
}
