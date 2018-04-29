namespace SU_Learning_System.Entities.Trainers
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;

    using Interfaces;
    using Parents.Trainer;
    
    internal class SeniorTrainer : Trainer, ISeniorTrainer
    {
        internal SeniorTrainer(string fName, string lName, int age, List<string> coursesLed)
            : base(fName, lName, age, coursesLed) { }

        internal SeniorTrainer(string fName, string lName, int age)
            : base(fName, lName, age) { }

        public void DeleteCourse(string courseName)
        {
            if (this.CoursesLed.Contains(courseName))
                this.CoursesLed.Remove(courseName);

            Console.WriteLine("The course {0} has been deleted.", courseName);
        }

        public override string ToString()
        {
            StringBuilder trainerInfo = new StringBuilder();

            trainerInfo.AppendLine("Senior Trainer: " + this.FirstName + " " + this.LastName);
            trainerInfo.AppendLine("Age: " + this.Age);
            trainerInfo.AppendLine("Courses Led: ");
            this.CoursesLed.ToList().ForEach(c => trainerInfo.AppendLine(c));

            return trainerInfo.ToString();
        }
    }
}
