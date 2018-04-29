namespace TheSlum.Entities.Boosts
{
    using Parents;

    class Injection : Bonus
    {
        private const int InjectionHealthEffect = 200;
        private const int InjectionTimeOut = 3;

        public Injection(string id) : this(id, InjectionHealthEffect, 0, 0)
        {

        }

        private Injection(string id, int healthEffect, int defenseEffect, int attackEffect) 
            : base(id, healthEffect, defenseEffect, attackEffect)
        {
        }
    }
}
