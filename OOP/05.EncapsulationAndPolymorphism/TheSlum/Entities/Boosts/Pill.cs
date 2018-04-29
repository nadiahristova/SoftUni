namespace TheSlum.Entities.Boosts
{
    using Parents;

    class Pill : Bonus
    {
        private const int PillAttackEffect = 100;
        private const int PillTimeOut = 1;

        public Pill(string id) : this(id, 0, 0, PillAttackEffect)
        {

        }

        private Pill(string id, int healthEffect, int defenseEffect, int attackEffect)
            : base(id, healthEffect, defenseEffect, attackEffect)
        {
        }
    }
}
