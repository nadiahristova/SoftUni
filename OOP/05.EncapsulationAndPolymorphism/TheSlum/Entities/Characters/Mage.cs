namespace TheSlum.Entities
{
    using Enums;
    using Parents;
    using Interfaces;

    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Mage : Character, IAttack
    {
        private const int Health = 150;
        private const int Defense = 50;
        private const int Attack = 300;
        private const int MageRange = 5;

        public Mage(string id, int x, int y, Team team) : this(id, x, y, Health, Defense, team, MageRange)
        {
            this.AttackPoints = Attack;
        }

        private Mage(string id, int x, int y, int healthPoints = Health, int defensePoints = Defense, Team team = Team.Blue, int range = MageRange) 
            : base(id, x, y, healthPoints, defensePoints, team, range)
        {
            this.AttackPoints = Attack;
        }

        public int AttackPoints { get; set; }


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
            return targetsList.Where(t => t.IsAlive && this.Team != t.Team)
                .OrderByDescending(t => this.CalculateDistance(t))
                .FirstOrDefault(t => this.CalculateDistance(t) <= this.Range);
        }

        private double CalculateDistance(Character other)
        {
            return Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        }

        public override string ToString()
        {
            return base.ToString() + $", Attack: {this.AttackPoints}";
        }
    }
}
