namespace SU_Learning_System.Interfaces
{
    using System.Collections.Generic;

    public interface ITrainer : IPerson
    {
        ICollection<string> CoursesLed { get; set; }

        string CreateCourse(string courseName);

        void PrintOwnCourses();
    }
}
