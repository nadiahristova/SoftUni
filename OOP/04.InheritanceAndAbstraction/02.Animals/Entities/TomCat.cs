namespace Animals.Entities
{
    using System;

    using Parents;
    using Animals.Enumerators;

    class TomCat : Cat
    {
        public TomCat(string name, byte age) : base(name, age)
        { }        

        public Gender Gender
        {
            get { return Gender.Male; }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("Huh? Food! Now!");
        }

        public override string ToString()
        {
            return "Tomcat " + base.ToString() + ". Gender: " + this.Gender.ToString();
        }
    }
}
