namespace Animals.Entities
{

    using System;

    using Parents;
    using Enumerators;

    class Kitten : Cat
    {
        public Kitten(string name, byte age) : base(name, age)
        { }

        public Gender Gender
        {
            get { return Gender.Female; }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("May may.");
        }

        public override string ToString()
        {
            return "Kitty " + base.ToString() + ". Gender: " + this.Gender.ToString();
        }
    }
}
