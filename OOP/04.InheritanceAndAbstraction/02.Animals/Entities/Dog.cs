namespace Animals.Entities
{
    using System;

    using Enumerators;
    using Parents.Progenitor;

    class Dog : Animal
    {
        private Gender gender;

        public Dog(string name, byte age, Gender gender) : base(name, age)
        {
            this.Gender = gender;
        }

        public override Gender Gender { get; set; }

        public override void ProduceSound()
        {
            Console.WriteLine("Bay Bay.");
        }

        public override string ToString()
        {
            return "Dog " + base.ToString() + ". Gender: " + this.Gender.ToString();
        }
    }
}
