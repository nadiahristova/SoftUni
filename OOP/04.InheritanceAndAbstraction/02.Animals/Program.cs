namespace Animals
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Animals.Entities;
    using Animals.Entities.Parents.Progenitor;
    using Animals.Enumerators;

    class Program
    {
        private const int MaxCatAge = 15;
        private const int MaxDogAge = 22;
        private const int MAxFrogAge = 2;

        private static List<string> animalNames = 
            new List<string>() { "Fifi", "Jorko", "Rex", "Приоданец", "Kalinka", "Sinthia", "Belcho", "Belodrejko", "Topsi", "Ей-ти-махай-се-от-тука", "Santiago", "Charlina" };

        static void Main()
        {
            ICollection<Animal> animalsList = SeedAnimals();

            animalsList.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();

            Type[] arrayOfDerivedTypes = FindDerivedTypes(typeof(Animal));

            foreach(var type in arrayOfDerivedTypes)
            {
                var objOfType = animalsList.Where(a => a.GetType() == type);
                if (objOfType != null && objOfType.Any())
                {
                    double avgAge = objOfType.Average(a => a.Age);
                    Console.WriteLine($"Average {type.Name} age is: {avgAge} years.");
                }                   
            }
        }

        private static Type[] FindDerivedTypes(Type type)
        {
            return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where type.IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();            
        }

        private static ICollection<Animal> SeedAnimals()
        {
            ICollection<Animal> animals = new List<Animal>();

            var rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                string name = animalNames[rnd.Next(0, animalNames.Count)];
                animalNames.Remove(name);

                var doggie = new Dog(
                        name,
                        (byte)rnd.Next(0, MaxDogAge),
                        (Gender)rnd.Next(0, Enum.GetNames(typeof(Gender)).Length)
                    );

                animals.Add(doggie);
            }

            for (int i = 0; i < 2; i++)
            {
                string name = animalNames[rnd.Next(0, animalNames.Count)];
                animalNames.Remove(name);

                var kitten = new Kitten(
                        name,
                        (byte)rnd.Next(0, MaxCatAge)
                    );

                animals.Add(kitten);
            }

            for (int i = 0; i < 2; i++)
            {
                string name = animalNames[rnd.Next(0, animalNames.Count)];
                animalNames.Remove(name);

                var tomCat = new TomCat(
                        name,
                        (byte)rnd.Next(0, MaxCatAge)
                    );

                animals.Add(tomCat);
            }

            for (int i = 0; i < 4; i++)
            {
                string name = animalNames[rnd.Next(0, animalNames.Count)];
                animalNames.Remove(name);

                var froggie = new Frog(
                        name,
                        (byte)rnd.Next(0, MAxFrogAge),
                        (Gender)rnd.Next(0, Enum.GetNames(typeof(Gender)).Length)
                    );

                animals.Add(froggie);
            }

            return animals;
        }
    }
}
