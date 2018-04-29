namespace TheSlum.Entities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Parents;
    using Interfaces;
    using Enums;

    class Healer : Character, IHeal
    {
        private const int Health = 75;
        private const int Defense = 50;
        private const int Heal = 60;
        private const int HealRange = 6;

        public Healer(string id, int x, int y, Team team ) : this(id, x, y, Health, Defense, team, HealRange)
        {
            this.HealingPoints = Heal;
        }

        private Healer(string id, int x, int y, int healthPoints, int defensePoints, Team team, int range) 
            : base(id, x, y, healthPoints, defensePoints, team, range)
        {  }

        public int HealingPoints { get; set; }

        public override void AddToInventory(Item item)
        {
            this.Inventory.Add(item);
        }

        public override void RemoveFromInventory(Item item)
        {
            this.Inventory.Remove(item);
        }

        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            return targetsList.Where(t => t.IsAlive && this.Team == t.Team && t.Id != this.Id)
                .OrderBy(t => t.HealthPoints)
                .FirstOrDefault(t => this.CalculateDistance(t) <= this.Range);
        }

        private double CalculateDistance(Character other)
        {
            return Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        }

        public override string ToString()
        {
            return base.ToString() + $", Healing: {this.HealingPoints}";
        }
    }
}
