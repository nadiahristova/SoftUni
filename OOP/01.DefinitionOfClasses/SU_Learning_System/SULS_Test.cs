namespace SU_Learning_System
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using SU_Learning_System.Entities.Students;
    using SU_Learning_System.Entities.Trainers;    
    using SU_Learning_System.Entities.Parents.Student;
    using SU_Learning_System.Entities.Parents.Progenitor;

    class SULS_Test
    {
        static void Main()
        {
            DropoutStudent Ivancho = new DropoutStudent("Ivan", "Ivanov", 56, "100323112", 5.6f, 
                "Unable to finish OOP course. Failed to show up at the final exam. Personal matters.");
            Ivancho.Reapply();
            Console.WriteLine();
            SeniorTrainer Mite = new SeniorTrainer("Mitko", "Leshtarov", 30, new List<string> { "Animation", "Mathematics" });
            Mite.CreateCourse("History");
            Mite.DeleteCourse("Animation");
            Mite.PrintOwnCourses();

            List<Person> listOfIndividuals = new List<Person>()
            {
                new JuniorTrainer("Goshko", "Goshkov", 23, new List<string>{"Music", "Mathematics"}),
                new JuniorTrainer("Petko", "Nikolov", 19, new List<string>{"OOP", "Drawing"}),
                new SeniorTrainer("Mitko", "Leshtarov", 30, new List<string>{"Animation", "Mathematics"}),
                new JuniorTrainer("Tonkcho", "Dimitrov", 22, new List<string>{"Biology"})
            };

            var rnd = new Random();

            int strNumber = 123002;
            var firstNames = new List<string>()
            {
                "Goshko", "Petko", "Mitko", "Tonkcho", "Nikola", "Lidia", "Vesela", "Petar", "Mitko", "Pencho", 
                "Nikoleta", "Petraki", "Leni", "Vladimir", "Valyo", "Fatim", "Mirela", "Kliment"
            };
            var lastNames = new List<string>()
            {
                "Goshkov/a", "Leshtarov/a", "Dimitrov/a", "Hristov", "Georgiev/a", "Ivanov/a", "Grozdanov/a", "Penchev/a", "Kravitz", "Totev/a", "Fedorov/a"
            };
            var reasons = new List<string>()
            {
                "Myrzel", "Malko po malyk myrzel", "Gadzheto zamina v chujbina", "Propusnat izpit po Java", "Ne si vzeh OOP-to.", "Ivan Ionkov =)"
            };
            var classes = new List<string>()
            {
                "Music", "Mathematics", "OOP", "Drawing", "Animation", "Mathematics", "Biology"
            };

            for (int i = 0; i < rnd.Next(3,9); i++)
			{
                listOfIndividuals.Add(new GraduateStudent(firstNames[rnd.Next(0, firstNames.Count)], lastNames[rnd.Next(0, lastNames.Count)], rnd.Next(10, 71), (++strNumber).ToString(), (float)(rnd.NextDouble() * (6 - 2) + 2)));
			}

            for (int i = 0; i < rnd.Next(3, 19); i++)
            {
                listOfIndividuals.Add(new DropoutStudent(firstNames[rnd.Next(0, firstNames.Count)], lastNames[rnd.Next(0, lastNames.Count)], rnd.Next(10, 71), (++strNumber).ToString(),
                    (float)(rnd.NextDouble() * (6 - 2) + 2), reasons[rnd.Next(0, reasons.Count)]));
            }

            for (int i = 0; i < rnd.Next(2, 7); i++)
            {
                listOfIndividuals.Add(new OnlineStudent(firstNames[rnd.Next(0, firstNames.Count)], lastNames[rnd.Next(0, lastNames.Count)], rnd.Next(10, 71), (++strNumber).ToString(),
                    (float)(rnd.NextDouble() * (6 - 2) + 2), classes[rnd.Next(0, classes.Count)]));
            }

            for (int i = 0; i < rnd.Next(2, 7); i++)
            {
                listOfIndividuals.Add(new OnsiteStudent(firstNames[rnd.Next(0, firstNames.Count)], lastNames[rnd.Next(0, lastNames.Count)], rnd.Next(10, 71), (++strNumber).ToString(),
                    (float)(rnd.NextDouble() * (6 - 2) + 2), classes[rnd.Next(0, classes.Count)], rnd.Next(0, 255)));
            }

            Console.WriteLine("\nOverall number of individuals in the university: " + Person.NumPersons);
            Console.WriteLine("\nOverall number of active students: " + CurrentStudent.NumCurrentStudents + "\n");

            List<Person> currStudents = listOfIndividuals.Where(x => x is CurrentStudent).
                OrderBy(x => (x as CurrentStudent).AvgGrade).ToList();

            currStudents.ForEach(Console.WriteLine);
        }
    }
}
