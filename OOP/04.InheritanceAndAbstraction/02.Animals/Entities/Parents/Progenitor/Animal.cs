namespace Animals.Entities.Parents.Progenitor
{
    using Interfaces;
    using Enumerators;

    abstract class Animal : ISoundProducable
    {
        protected Animal(string name, byte age)
        {
            this.Name = name;
            this.Age = age;
        }

        protected Animal(string name, byte age, Gender gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public byte Age { get; set; }

        public string Name { get; set; }

        public virtual Gender Gender { get; set; }
        
        public abstract void ProduceSound();

        public override string ToString()
        {
            return this.Name + ", age: " + this.Age;
        }
    }
}
