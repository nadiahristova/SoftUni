using StudentSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using StudentSystem.Model;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Studentsystem.ConsoleClient
{
    class StudentsMain
    {
        static void Main()
        {
            var ctx = new StudentContext();

            // Task 3
            //step 1 - Lists all students and their homework submissions. Select only their 
            //names and for each homework - content and content-type.
            Console.WriteLine("Task 3\n");
            var studentswithHomeworks = ctx.Homeworks.Select(h =>
                new
                {
                    StudentName = h.Student.Name,
                    Content = h.Content,
                    ContType = h.ContentType
                }).GroupBy(n => n.StudentName);

            foreach (var student in studentswithHomeworks)
            {
                Console.WriteLine("Student's name: {0}", student.Key);
                foreach(var homework in student)
                {
                    Console.WriteLine("  Homework Content: " + homework.Content + "   Type: " + homework.ContType);
                }
            }

            Console.WriteLine("\n\n");

            //step 2 - List all courses with their corresponding resources. Select the 
            //course name and description and everything for each resource. Order the courses 
            //by start date (ascending), then by end date (descending).

            var allCoursesWithResourses = ctx.Courses.OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                    {
                        CourseName = c.Name,
                        CourseDescription = c.Description,
                        Resourcess = c.Resources
                    });

            foreach (var course in allCoursesWithResourses)
            {
                Console.WriteLine("Course name: " + course.CourseName);
                var courseDescription = course.CourseDescription == null ? "nope" : course.CourseDescription;
                Console.WriteLine("Course description: " + courseDescription);
                if (course.Resourcess.Count() != 0)
                    foreach (var resource in course.Resourcess)
                    {
                        Console.WriteLine("Resource name: {0}, Type: {1}, URL: {2}",
                            resource.Name, resource.Type, resource.URL);
                    }
                else Console.WriteLine("The course has no resources :) use Bat Google.");
                Console.WriteLine();
            }            
            Console.WriteLine("\n\n");

            //step 3 - I am using more than 2 instead of more than 5, beckause my data is not enough
            //List all courses with more than 5 resources. Order them by resources count (descending),
            //then by start date (descending). Select only the course name and the resource count.
            var coursesWithMoreThan2Resources = ctx.Courses.Where(c => c.Resources.Count >= 2)
                .OrderByDescending(c => c.Resources.Count())
                .ThenByDescending(c => c.StartDate).Select(c => new 
                {
                    c.Name,
                    ResourcesCount = c.Resources.Count()
                });

            foreach (var course in coursesWithMoreThan2Resources)
            {
                Console.WriteLine(course.Name + ": " + course.ResourcesCount);
            }
            Console.WriteLine("\n\n");

            //step 4 - List all courses which were active on a given date (choose the date depending on the data 
            //seeded to ensure there are results), and for each course count the number of students enrolled. 
            //Select the course name, start and end date, course duration (difference between end and start date) 
            //and number of students enrolled. Order the results by the number of students enrolled (in descending order), then by duration (descending).

            var givenDate = new DateTime(2001, 3, 5);
            var allCoursesThatAreActive = ctx.Courses.Where(c => c.StartDate <= givenDate && c.EndDate >= givenDate)
                .Select(c => new 
                {
                    CourseName = c.Name,
                    c.StartDate,
                    c.EndDate,
                    CourseDuration = EntityFunctions.DiffMinutes(c.StartDate, c.EndDate),
                    NumOfStudentsEnroll = c.Students.Count()
                }).OrderByDescending(c => c.NumOfStudentsEnroll).ThenByDescending(c => c.CourseDuration);

            foreach (var course in allCoursesThatAreActive)
            {
                Console.WriteLine("Course Name: {0}, Start Date: {1}, End Date {2}, Students Enrolled: {3}, Duration: {4} minutes :)\n",
                    course.CourseName, String.Format("{0:d/M/yyyy HH:mm:ss}", course.StartDate), String.Format("{0:d/M/yyyy HH:mm:ss}", course.EndDate), course.NumOfStudentsEnroll, course.CourseDuration);
            }
            Console.WriteLine("\n\n");

            //step 5 - For each student, calculate the number of courses he/she has enrolled in, the total 
            //price of these courses and the average price per course for the student.
            //Select the student name, number of courses, total price and average price. Order the results by total price 
            //(descending), then by number of courses (descending) and then by the student's name (ascending).

            var infoAboutStudents = ctx.Students.Select(s => new 
            {
                s.Name,
                NumOFCourses = s.Courses.Count(),
                TotalPricePaid = s.Courses.Sum(c => c.Price),
                AvgPrice = s.Courses.Average(c => c.Price)
            }).OrderByDescending(s => s.TotalPricePaid).ThenByDescending(s => s.AvgPrice);

            foreach (var student in infoAboutStudents)
            {
                Console.WriteLine("Student Name: {0}, Num Courses: {1}, Total Price {2} lv, Avg Price: {3} lv",
                    student.Name, student.NumOFCourses, student.TotalPricePaid, student.AvgPrice);
            }
        }
    }
}
