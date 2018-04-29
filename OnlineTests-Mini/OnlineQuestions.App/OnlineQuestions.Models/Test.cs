using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuestions.Models
{
    public class Test
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
            this.UserDoesTest = new HashSet<UserDoesTest>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "The Tests title must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string Title { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<UserDoesTest> UserDoesTest { get; set; }
    }
}
