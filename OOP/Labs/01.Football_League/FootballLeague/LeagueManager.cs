using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague
{
    using Models;

    using System;    
    using System.Linq;
    using System.Text.RegularExpressions;

    class LeagueManager
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();

            while (line != "End")
            {
                try
                {
                    LeagueManager.HandleInput(line);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }

                line = Console.ReadLine();
            }
        }

        private static void HandleInput(string input)
        {
            var inputArgs = Regex.Split(input, @"\s+\|\s+");

            switch(inputArgs[0])
            {
                case "AddTeam":
                    AddTeam(inputArgs[1], inputArgs[2], DateTime.Parse(inputArgs[3]));
                    break;
                case "AddMatch":
                    AddMatch(inputArgs[1], inputArgs[2], int.Parse(inputArgs[3]), int.Parse(inputArgs[4]));
                    break;
                case "AddPlayerToTeam":
                    AddPlayerToTeam(inputArgs[1], inputArgs[2], Decimal.Parse(inputArgs[3]), DateTime.Parse(inputArgs[4]), inputArgs[5]);
                    break;
                case "ListTeams":
                    ListTeams();
                    break;
                case "ListMatches":
                    ListMatches();
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation: " + inputArgs[0]);
            }
        }

        private static void ListMatches()
        {
            League.Matches.ToList().ForEach(Console.WriteLine);
        }

        private static void ListTeams()
        {
            League.Teams.ToList().ForEach(Console.WriteLine);
        }

        private static void AddPlayerToTeam(string firstName, string lastName, decimal salary, DateTime dateOfBirth, string team)
        {
            Team targetTeam = League.Teams.FirstOrDefault(t => t.Name == team);

            if (targetTeam == null)
                throw new ArgumentException(team + " does not exists in this league.");

            Player newPlayer = new Player(firstName, lastName, salary, dateOfBirth);

            targetTeam.AddPlayer(newPlayer);
        }

        private static void AddMatch(string homeTeam, string awayTeam, int homeTeamGoals, int awayTeamGoals)
        {
            Team leagueHomeTeam = League.Teams.FirstOrDefault(t => t.Name == homeTeam);
            Team awayHomeTeam = League.Teams.FirstOrDefault(t => t.Name == awayTeam);

            if (leagueHomeTeam == null || awayHomeTeam == null)
                throw new ArgumentException("Home and/or away team do/does not exists in this league.");

            var newMatch = new Models.Match(leagueHomeTeam, awayHomeTeam, new Score(homeTeamGoals, awayTeamGoals));
            League.AddMatch(newMatch);

            Console.WriteLine(string.Format("{0} - {1} match successfully created.", homeTeam, awayTeam));
        }

        private static void AddTeam(string name, string nickName, DateTime dateFounded)
        {
            Team newTeam = new Team(name, nickName, dateFounded);

            League.AddTeam(newTeam);

            Console.WriteLine(string.Format("Team {0} ({1}) successfully added to league.", name, nickName));
        }
    }
}
