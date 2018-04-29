namespace CustomLINQExtension
{
    using System;    
    using System.Collections.Generic;
    
    using Entities;
    using Extensions;

    class TestCustomLINQ
    {
        static void Main()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Func<int, bool> predicate = (num) 
                => 
                    {
                        return num % 2 == 0;
                    };

            var filteredColl = nums.WhereNot(predicate);

            Console.WriteLine(string.Join(", ", filteredColl));

            var students = new List<Student>
            {
                new Student("Pesho", 3),
                new Student("Gosho", 2),
                new Student("Mariika", 7),
                new Student("Stamat", 5),
            };

            Console.WriteLine($"\nMax grade is: {students.Max(student => student.Grade)}");
        }
    }
}
