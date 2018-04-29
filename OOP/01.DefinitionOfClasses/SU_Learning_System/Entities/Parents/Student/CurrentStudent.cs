namespace SU_Learning_System.Entities.Parents.Student
{    
    using System;

    using SU_Learning_System.Interfaces;

    internal abstract class CurrentStudent : Student, ICurrentStudent
    {        
        private string currentCourse;

        public static int NumCurrentStudents = 0;

        protected CurrentStudent(string fName, string lName, int age, 
            string studentNumber, float? avgGrade, string currentCourse)
            : base(fName, lName, age, studentNumber, avgGrade)
        {
            this.CurrentCourse = currentCourse;

            CurrentStudent.NumCurrentStudents++;
        }

        public string CurrentCourse
        {
            get { return this.currentCourse; }
            set 
                { 
                    if(string.IsNullOrEmpty(value))
                        throw new ArgumentException("Try to re-enter the name's course.");

                    this.currentCourse = value;
                 }
        }        
    }
}
