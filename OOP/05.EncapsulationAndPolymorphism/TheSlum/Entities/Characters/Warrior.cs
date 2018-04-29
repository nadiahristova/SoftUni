namespace TheSlum.Entities
{
    using System.Linq;
    using System.Collections.Generic;

    using Parents;
    using Interfaces;
    using Enums;
    using System;

    class Warrior : Character, IAttack
    {
        private const int Health = 200;
        private const int Defense = 100;
        private const int Attack = 150;
        private const int WarRange = 2;


        public Warrior(string id, int x, int y, Team team) : this(id, x, y, Health, Defense, team, WarRange)
        {
            this.AttackPoints = Attack;
        }

        private Warrior(string id, int x, int y, int healthPoints = Health, int defensePoints = Defense, Team team = Team.Blue, int range = WarRange) 
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
                .OrderBy(t => this.CalculateDistance(t))
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
