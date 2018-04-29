namespace SU_Learning_System.Entities.Parents.Trainer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    using SU_Learning_System.Interfaces;
    using SU_Learning_System.Entities.Parents.Progenitor;

    public abstract class Trainer : Person, ITrainer
    {
        private ICollection<string> coursesLed;

        public static int NumTrainers = 0;

        protected Trainer(string fName, string lName, int age, ICollection<string> coursesLed) 
            : base(fName, lName, age)
        {
            this.CoursesLed = coursesLed;

            Trainer.NumTrainers++;
        }

        protected Trainer(string fName, string lName, int age)
            : this (fName, lName, age, new HashSet<string>())
        { }

        public ICollection<string> CoursesLed
        {
            get { return this.coursesLed; }
            set { this.coursesLed = value; }
        }

        public string CreateCourse(string courseName)
        {
            this.CoursesLed.Add(courseName);
            Console.WriteLine("Course " + courseName + " has been created.");
            return "Course " + courseName + " has been created.";
        }

        public void PrintOwnCourses()
        {
            this.coursesLed.ToList().ForEach(Console.WriteLine);
        }
    }
}
