namespace SU_Learning_System.Entities.Trainers
{
    
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using SU_Learning_System.Entities.Parents.Trainer;   
     
    internal class JuniorTrainer : Trainer
    {
        internal JuniorTrainer(string fName, string lName, int age, List<string> coursesLed)
            : base(fName, lName, age, coursesLed) { }

        internal JuniorTrainer(string fName, string lName, int age)
            : base(fName, lName, age) { }

        public override string ToString()
        {
            StringBuilder trainerInfo = new StringBuilder();

            trainerInfo.AppendLine("Junior Trainer: " + this.FirstName + " " + this.LastName);
            trainerInfo.AppendLine("Age: " + this.Age);
            trainerInfo.AppendLine("Courses Led: ");
            this.CoursesLed.ToList().ForEach(c => trainerInfo.AppendLine(c));

            return trainerInfo.ToString();
        }
    }
}
