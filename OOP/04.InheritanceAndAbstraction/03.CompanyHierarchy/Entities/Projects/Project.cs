namespace CompanyHierarchy.Entities.Projects
{
    using System;
    using System.Text;

    using Enums;
    using Interfaces;

    class Project : IProject, IEquatable<Project>
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 55;
        private static readonly DateTime minStartDate = new DateTime(1995, 1, 1, 0, 0, 0);

        private string name;
        private DateTime startDate;
        
        public Project(string name, DateTime startDate, string details = null)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.Details = details;
        }

        public Project(string name, DateTime startDate, State state, string details = null) : this(name, startDate, details)
        {
            this.State = state;
        }

        public string Details { get; set; } //Can be null

        public string Name
        {
            get { return this.name; }
            set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException("Project name is required.");

                    if (value.Length < MinNameLength || value.Length > MaxNameLength)
                        throw new ArgumentOutOfRangeException($"Project's name length shpuld be in the boundaries: [{MinNameLength}, {MaxNameLength}]");

                    this.name = value;
                }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            set
                {
                    if (value.CompareTo(minStartDate) < 0)
                        throw new ArgumentOutOfRangeException($"Project with start date earlier than {minStartDate.ToString("dd.MM, yyyy")} are considured out of date.");

                    this.startDate = value;
                }
        }

        public State State { get; set; } //if is not set the state will always be Open

        public void CloseProject()
        {
            if (this.State == State.Closed)
                throw new ArgumentException("The project has been already closed.");

            this.State = State.Closed;
        }

        public override string ToString()
        {
            var projectInfo = new StringBuilder();

            projectInfo.AppendLine($"Project name: {this.Name}, status: {this.State}");
            projectInfo.AppendLine($"Start date: {this.StartDate.ToString("dd.MMM, yyyy")}");
            projectInfo.AppendLine("Details:");
            projectInfo.AppendLine(this.Details ?? "[none given]");

            return projectInfo.ToString();
        }

        public bool Equals(Project other)// we need this because we use HashSet
        {
            return this.Name.Trim() == other.Name.Trim();
        }
    }
}
