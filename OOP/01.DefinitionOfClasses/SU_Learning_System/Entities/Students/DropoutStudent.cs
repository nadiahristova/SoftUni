namespace SU_Learning_System.Entities.Students
{
    using System;    
    using System.Text;

    using SU_Learning_System.Interfaces;
    using SU_Learning_System.Entities.Parents.Student; 
       
    internal class DropoutStudent : Student, IReapplay
    {
        private string dropoutReason;

        public static int NumDropoutStudents = 0;
        internal string DropoutReason
        {
            get { return this.dropoutReason; }
            set
                {
                    if(string.IsNullOrEmpty(value) || value.Length < 5)
                        throw new ArgumentException("Invalid dropout reason.");

                    this.dropoutReason = value;
                }
        }

        internal DropoutStudent(string fName, string lName, int age,
            string studentNumber, float? avgGrade, string dropoutReason)
            : base(fName, lName, age, studentNumber, avgGrade) 
        {
            this.DropoutReason = dropoutReason;

            DropoutStudent.NumDropoutStudents++;
        }

        public string Reapply()
        {
            string form = this.ToString() + "\n" + string.Format("Dropout reason: {0}", this.dropoutReason);
            Console.WriteLine(form);

            return form;
        }

        public override string ToString()
        {
            StringBuilder studentInfo = new StringBuilder();

            studentInfo.AppendLine("Former Student: " + this.FirstName + " " + this.LastName);
            studentInfo.AppendLine("Student's number: " + this.StudentNumber);
            studentInfo.AppendLine("Age: " + this.Age);
            studentInfo.AppendLine("Average Grade: " + this.AvgGrade ?? "[no exams taken]");

            return studentInfo.ToString();
        }
    }
}
