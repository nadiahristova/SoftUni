namespace FootballLeague.Models
{
    using System;
    using System.Text;

    public class Player
    {
        private static readonly DateTime MaxAllowedDateOfBirth = new DateTime(1980, 1, 1, 0, 0, 0);

        private string firstName;
        private string lastName;

        private decimal salary;

        private DateTime dateOfBirth;

        private Team team;

        public Player(string firstName, string lastName, decimal salary, DateTime dateOfBirth, Team team)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.DateOfBirth = dateOfBirth;
            this.Team = team;
        }

        public Player(string firstName, string lastName, decimal salary, DateTime dateOfBirth) 
            : this (firstName, lastName, salary, dateOfBirth, null)
        { }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Player's first name is required.");
                }

                if (value.Length < 3)
                {
                    throw new ArgumentException("Player's first name should be at least 3 characters long.");
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Player's last name is required.");
                }

                if (value.Length < 3)
                {
                    throw new ArgumentException("Player's last name should be at least 3 characters long.");
                }

                this.lastName = value;
            }
        }

        public decimal Salary
        {
            get { return this.salary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Salary cannot be negative.");
                }

                this.salary = value;
            }
        }

        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (DateTime.Compare(MaxAllowedDateOfBirth, value) > 0)
                {
                    throw new ArgumentOutOfRangeException("Player's date of birth must be earlier than " + MaxAllowedDateOfBirth.ToString("dd.MMMM, yyyy") + ".");
                }

                this.dateOfBirth = value;
            }
        }

        public Team Team
        {
            get { return this.team; }
            set { this.team = value; }
        }

        public override string ToString()
        {
            var playerInfo = new StringBuilder();

            playerInfo.AppendLine(string.Format("Player: {0} {1}", this.firstName, this.lastName));
            playerInfo.AppendLine(string.Format("Born on: {0}", this.dateOfBirth));
            playerInfo.AppendLine(string.Format("Currently plays for: {0} with salary: {1}", this.Team.NickName, string.Format("{0:0.00} $", this.salary)));

            return playerInfo.ToString();
        }
    }
}
