using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class License
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        private ICollection<Resource> resources;
        public License()
        {
            this.Resources = new HashSet<Resource>();
        }
        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }
    }
}
