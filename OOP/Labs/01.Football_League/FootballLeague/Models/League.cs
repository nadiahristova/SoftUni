namespace FootballLeague.Models
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;    

    static class League
    {
        private static ICollection<Match> matches = new HashSet<Match>();
        private static ICollection<Team> teams = new HashSet<Team>();

        public static ICollection<Match> Matches
        {
            get { return matches; }
        }

        public static ICollection<Team> Teams
        {
            get { return teams; }
        }

        public static void AddTeam(Team team)
        {
            if (CheckIfTeamExists(team))
                throw new InvalidOperationException("Team already exists in this league.");

            teams.Add(team);
        }

        private static bool CheckIfTeamExists(Team team)
        {
            return teams.Any(t => t.Name == team.Name);
        }

        public static void AddMatch(Match match)
        {
            if (CheckIfMatchExists(match))
                throw new InvalidOperationException("Match already exists in the league.");

            matches.Add(match);
        }

        private static bool CheckIfMatchExists(Match match)
        {
            return matches.Any(m => m.Id == match.Id);
        }

        public static string Info()
        {
            var leagueInfo = new StringBuilder();

            leagueInfo.AppendLine("Matches played:");
            string matchesPlayed = Matches.Count() == 0 ? "[no matches at the moment]" :
                string.Join(", ", Matches.Select(m => m.HomeTeam.Name + " - " + m.AwayTeam.Name));
            leagueInfo.AppendLine(matchesPlayed);

            leagueInfo.AppendLine("Teams participating:");
            string teamsParticipationg = Teams.Count() == 0 ? "[no teams at the moment]" :
                string.Join(", ", Teams.Select(t => t.Name));
            leagueInfo.AppendLine(teamsParticipationg);

            return leagueInfo.ToString();
        }
    }
}
