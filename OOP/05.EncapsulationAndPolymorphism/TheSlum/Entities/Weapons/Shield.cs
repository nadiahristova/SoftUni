namespace TheSlum.Entities.Weapons
{
    using Parents;

    class Shield : Item
    {
        private const int ShieldHealthEffect = 0;
        private const int ShieldDefenseEffect = 0;
        private const int ShieldAttackEffect = 75;

        private Shield(string id, int healthEffect, int defenseEffect, int attackEffect)
            : base(id, healthEffect, defenseEffect, attackEffect)
        {
        }

        public Shield(string id) : this(id, ShieldHealthEffect, ShieldDefenseEffect, ShieldAttackEffect)
        { }
    }
}
