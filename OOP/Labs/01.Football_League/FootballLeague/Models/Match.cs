namespace FootballLeague.Models
{
    using System;
    using System.Text;

    public class Match
    {
        private int id;

        private Team homeTeam;
        private Team awayTeam;

        private Score score;
        
        public Match(Team homeTeam, Team awayTeam, Score score)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.Score = score;
        }

        public Match(Team homeTeam, Team awayTeam) 
            : this (homeTeam, awayTeam, null)
        { }

        public int Id
		{
            get { return this.id; }
            set { this.id = value; }
        }

        public Team HomeTeam
        {
            get { return this.homeTeam; }
            set { this.homeTeam = value; }
        }

        public Team AwayTeam
        {
            get { return this.awayTeam; }
            set { this.awayTeam = value; }
        }

        public Score Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public Team GetWinner()
        {
            if (this.IsDraw())
                return null;

            return this.Score.HomeTeamGoals > this.Score.AwayTeamGoals
                ? this.HomeTeam
                : this.AwayTeam;
        }

        private bool IsDraw()
        {
            return this.Score.AwayTeamGoals == this.Score.HomeTeamGoals;
        }

        public override string ToString()
        {
            var matchInfo = new StringBuilder();
            matchInfo.AppendLine(string.Format
                ("{0} - {1} : {2}", 
                    this.HomeTeam.Name, 
                    this.AwayTeam.Name,
                    this.Score.ToString()
                ));

            return matchInfo.ToString();
        }
    }
}
