namespace CompanyHierarchy.Entities.Persons
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Parents;
    using Projects;
    using Interfaces;

    class Developer : RegularEmployee, IDeveloper
    {
        public Developer(int id, string firstName, string lastName, IEnumerable<Project> projects) 
            : base(id, firstName, lastName)
        {
            this.Projects = projects;
        }

        public Developer(int id, string firstName, string lastName, params Project[] projects) 
            : base(id, firstName, lastName)
        {
            this.Projects = projects;
        }

        public Developer(int id, string firstName, string lastName) : this(id, firstName, lastName, new HashSet<Project>())
        { }

        public IEnumerable<Project> Projects { get; set; }

        public override string ToString()
        {
            var managerInfo = new StringBuilder();
            managerInfo.AppendLine($"{this.GetType().Name}: {base.ToString()}");
            managerInfo.AppendLine("Projects developed:");

            if (!this.Projects.Any())
                managerInfo.AppendLine("No projects so far.");
            else
                foreach (var project in this.Projects)
                    managerInfo.AppendLine(project.ToString());

            return managerInfo.ToString();
        }
    }
}
