namespace TheSlum.Entities.Weapons
{
    using Parents;

    class Axe : Item
    {
        private const int AxeHealthEffect = 0;
        private const int AxeDefenseEffect = 0;
        private const int AxeAttackEffect = 75;

        private Axe(string id, int healthEffect, int defenseEffect, int attackEffect) 
            : base(id, healthEffect, defenseEffect, attackEffect)
        {
        }

        public Axe(string id) : this(id, AxeHealthEffect, AxeDefenseEffect, AxeAttackEffect)
        {  }
    }
}
