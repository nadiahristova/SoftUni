namespace InheritanceAndAbstraction
{
    using System;
    using System.Linq;
    using Workers.Entities;
    using System.Collections.Generic;

    using Workers.Entities.Progenitor;

    class Program
    {
        private const int LengthFacNum = 10;
        private const int NumDigitsInFacNum = 3;

        private static readonly string[] firstNames = { "Kiril", "Didi", "Kiki", "Slavina", "Iliq" };
        private static readonly string[] lastNames = { "Stamenov/a", "Grigorov/a", "Tutev/a", "Simeonov/a" };

        private static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            ICollection<Student> setStudents = SeedStudents();
            ICollection<Worker> setWorkers = SeedWorkers();

            setStudents =  new HashSet<Student>(setStudents.OrderBy(st => st.FacultyNumber));
            setWorkers = setWorkers.OrderByDescending(w => w.MoneyPerHour()).ToList();

            IEnumerable<Human> allHumans = setWorkers.Concat<Human>(setStudents.ToList())
                .OrderBy(h => h.FirstName)
                .ThenBy(h => h.LastName);

            allHumans.ToList().ForEach(Console.WriteLine);
        }

        private static ICollection<Worker> SeedWorkers()
        {
            ICollection<Worker> workers = new List<Worker>();

            while (workers.Count() <= 10)
            {
                workers.Add(
                        new Worker(
                            firstNames[rnd.Next(0, firstNames.Length)],
                            lastNames[rnd.Next(0, lastNames.Length)],
                            (byte)rnd.Next(1, 9),
                            rnd.Next(200, 200000)
                        ));
            }

            return workers;
        }

        private static ICollection<Student> SeedStudents()
        {
            ICollection<Student> students = new HashSet<Student>();

            while (students.Count() <= 10)
            {
                students.Add(
                        new Student(
                            firstNames[rnd.Next(0, firstNames.Length)],
                            lastNames[rnd.Next(0, lastNames.Length)],
                            GenerateFacultyNumber(rnd)
                        ));
            }

            return students;
        }

        private static string GenerateFacultyNumber(Random rnd)
        {
            string facNum = null;

            for (int i = 0; i < NumDigitsInFacNum; i++)
            {
                facNum += (char)rnd.Next(65, 91);
            }

            facNum += rnd.Next((int)Math.Pow(10, LengthFacNum - NumDigitsInFacNum - 1), (int)Math.Pow(10, LengthFacNum - NumDigitsInFacNum));

            return facNum;
        }
    }
}
