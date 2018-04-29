namespace Minesweeper.Models
{
    using System;

    internal class Player
    {
        private string playerName;
        private int points;

        public Player()//?
        {
        }

        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

        public string Name
        {
            get { return playerName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Player's name is required."); 
                }

                this.playerName = value;
            }
        }

        public int Points
        {
            get { return points; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Points cannot be negative.");
                }

                points = value;
            }
        }            
    }
}
