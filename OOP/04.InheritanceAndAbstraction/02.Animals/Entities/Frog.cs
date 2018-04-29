namespace Animals.Entities
{
    using Parents.Progenitor;
    using Animals.Enumerators;
    using System;

    class Frog : Animal
    {
        private Gender gender;

        public Frog(string name, byte age, Gender gender) : base(name, age)
        {
            this.Gender = gender;
        }

        public override Gender Gender { get; set; }

        public override void ProduceSound()
        {
            Console.WriteLine("Quack quack.");
        }

        public override string ToString()
        {
            return "Frog " + base.ToString() + ". Gender: " + this.Gender.ToString();
        }
    }
}
