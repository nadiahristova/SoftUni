namespace StudentSystem.Data.Migrations
{
    using StudentSystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentSystem.Data.StudentContext context)
        {
            var ctx = new StudentContext();
            var rnd = new Random();

            if (ctx.Courses.Count() == 0)
                LoadCourses(ctx, rnd);

            if (ctx.Resources.Count() == 0)
                LoadResources(ctx, rnd);
            
            if (ctx.Students.Count() == 0)
                LoadStudents(ctx, rnd);

            if (ctx.Homeworks.Count() == 0)
                LoadHomeworks(ctx, rnd);
             
        }
        private void LoadCourses(StudentContext ctx, Random random)
        {
            using (var reader = new StreamReader(@"courses.txt"))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 4);
                    var startDate = DateTime.ParseExact(data[0], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var price = decimal.Parse(data[2], CultureInfo.InvariantCulture);
                    var name = data[3];
                    var c = new Course()
                    {
                        Name = name,
                        StartDate = startDate,
                        EndDate = endDate,
                        Price = price
                    };
                    ctx.Courses.Add(new Course()
                    {
                        Name = name,
                        StartDate = startDate,
                        EndDate = endDate,
                        Price = price
                    });

                    ctx.SaveChanges();
                    var ca = ctx.Courses.Count();
                    line = reader.ReadLine();
                }
            }
        }

        private void LoadResources(StudentContext ctx, Random random)
        {
            using (var reader = new StreamReader(@"resources.txt"))
            {
                string line = reader.ReadLine();
                var courses = ctx.Courses.ToArray();

                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 3);
                    var type = (ResourceType)int.Parse(data[0]);
                    var url = data[1];
                    var name = data[2];
                    int courseIndex = random.Next(1, courses.Length);
                    var course = courses[courseIndex];
                    var newResource = new Resource()
                    {
                        Name = name,
                        Course = course,
                        Type = type,
                        URL = url
                    };

                    course.Resources.Add(newResource);
                    ctx.Resources.Add(newResource);
                    line = reader.ReadLine();
                    ctx.SaveChanges();
                }
            }
        }

        private void LoadStudents(StudentContext ctx, Random random)
        {
            using (var reader = new StreamReader(@"students.txt"))
            {
                string line = reader.ReadLine();
                line = reader.ReadLine();
                var courses = ctx.Courses.ToArray();

                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 2);
                    var registeredOn = DateTime.ParseExact(data[0], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var name = data[1];
                    var attendedCourses = new HashSet<Course>();
                    var coursesToAttach = random.Next(0, 5);
                    while (coursesToAttach != 0)
                    {
                        int courseIndex = random.Next(1, courses.Length);
                        var course = courses[courseIndex];
                        attendedCourses.Add(course);
                        coursesToAttach--;
                    }

                    var newStudent = new Student()
                    {
                        Name = name,
                        RegisteredOn = registeredOn,
                        Courses = attendedCourses
                    };

                    foreach (var course in attendedCourses)
                    {
                        course.Students.Add(newStudent);
                    }

                    ctx.Students.Add(newStudent);
                    ctx.SaveChanges();
                    line = reader.ReadLine();
                }
            }
        }

         private void LoadHomeworks(StudentContext ctx, Random random)
        {
            using (var reader = new StreamReader(@"homeworks.txt"))
            {
                string line = reader.ReadLine();
                var courses = ctx.Courses.ToArray();
                var students = ctx.Students.ToArray();

                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 3);
                    var type = (ContentType)int.Parse(data[0]);
                    var submissionDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var content = data[2];
                    var studentIndex = random.Next(1, students.Length);
                    var courseIndex = random.Next(1, courses.Length);
                    var student = students[studentIndex];
                    var course = courses[courseIndex];

                    ctx.Homeworks.Add(new Homework()
                    {
                        Content = content,
                        SubmissionDate = submissionDate,
                        ContentType = type,
                        Course = course,
                        Student = student
                    });
                    line = reader.ReadLine();
                    ctx.SaveChanges();
                }
            }
        }
    }
}
