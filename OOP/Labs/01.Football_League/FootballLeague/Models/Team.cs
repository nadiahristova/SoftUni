namespace FootballLeague.Models
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;    

    public class Team
    {
        private string name;
        private string nickName;
        private ICollection<Player> players;

        public Team(string name, string nickName, DateTime dateFounded)
        {
            this.Name = name;
            this.NickName = nickName;
            this.DateFounded = dateFounded;
        }   

        public string Name
        {
            get { return this.name; }
            set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentNullException("Team name is required.");
                    }

                    if (value.Length < 5)
                    {
                        throw new ArgumentException("Team name should be at least 5 characters long.");
                    }

                    this.name = value;
                }
        }

        public string NickName
        {
            get { return this.nickName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Team name is required.");
                }

                if (value.Length < 5)
                {
                    throw new ArgumentException("Team nickname should be at least 5 characters long.");
                }

                this.nickName = value;
            }
        }

        public DateTime DateFounded { get; set; }

        public ICollection<Player> Players { get { return this.players; } }

        public void AddPlayer(Player player)
        {
            if (CheckIfPlayerExists(player))
                throw new InvalidOperationException("Player already exists for that team.");

            this.players.Add(player);
        }

        private bool CheckIfPlayerExists(Player player)
        {
            return this.players.Any(p => p.FirstName == player.FirstName &&
                p.LastName == player.LastName);
        }

        public override string ToString()
        {
            var teamInfo = new StringBuilder();

            teamInfo.AppendLine
            (
                string.Format("Team: {0} ({1}) - {2}", 
                    this.Name, 
                    this.NickName,
                    this.DateFounded.ToString("dd.MMMM, yyyy"))
            );

            string players = this.Players.Count() == 0 ? "[no players currently in the team]" : 
                string.Join(", ", this.Players.Select(p => p.FirstName + " " + p.LastName));

            teamInfo.AppendLine(string.Format("Players: {0}", players));

            return teamInfo.ToString();
        }
    }
}
