using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_and_Courses
{
    public class Program
    {
        private const string pathToFile = @"../../students1.txt";
        private static string tempCourseHolder;
        static void Main()
        {
            string[] inputData = File.ReadAllLines(pathToFile);
            var studentsAndCourses = new StudentCollection();

            foreach (var line in inputData)
            {
                Person currStudent = ExtractPersonFromString(line);
                studentsAndCourses.Add(currStudent, tempCourseHolder);
            }

            studentsAndCourses.Print();
        }

        private static Person ExtractPersonFromString(string line)
        {
            tempCourseHolder = null;

            var data = line.Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries);
            if (data.Count() != 3)
            {
                return null;
            }

            tempCourseHolder = data[2];

            return new Person()
                        {
                            FirstName = data[0].Trim(),
                            LastName = data[1].Trim()
                        };
        }
    }
}
