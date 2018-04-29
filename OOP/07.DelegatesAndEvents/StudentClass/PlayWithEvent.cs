namespace StudentClass
{
    using System;

    using CustomLINQExtension.Entities;    

    class PlayWithEvent
    {
        static void Main()
        {
            Student student = new Student("Peter", 4);
            student.PropertyChange += (sender, eventArgs) =>
            {
                Console.WriteLine("Property changed: {0} (from {1} to {2})",
                    eventArgs.PropertyName, eventArgs.OldValue, eventArgs.NewValue);
            };
            student.Name = "Maria";
            student.Grade = 5;
        }
    }
}
