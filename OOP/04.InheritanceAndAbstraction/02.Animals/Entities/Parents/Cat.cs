namespace Animals.Entities.Parents
{
    using Animals.Entities.Parents.Progenitor;

    abstract class Cat : Animal
    {
        protected Cat(string name, byte age) : base(name, age)
        {
        }        
    }
}
