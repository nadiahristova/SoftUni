using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_and_Courses
{
    class StudentCollection : IStudentCollection
    {
        private SortedDictionary<string, SortedSet<Person>> studentsByCourses = 
            new SortedDictionary<string, SortedSet<Person>>();
        public bool Add(Person student, string course)
        {
            return this.studentsByCourses.AppendValueToKey(course, student);
        }
        public void Print()
        {
            foreach(var courseAndStudent in studentsByCourses)
            {
                Console.WriteLine(courseAndStudent.Key + ": " + string.Join(", ", this.ReturnAllStudentsWithCourse(courseAndStudent.Key)));
            }
        }

        private IEnumerable<string> ReturnAllStudentsWithCourse(string course)
        {
            foreach(var student in studentsByCourses[course])
            {
                yield return student.ToString(); 
            }
        }
    }
}
