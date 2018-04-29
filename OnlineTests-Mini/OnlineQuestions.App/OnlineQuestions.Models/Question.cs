using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuestions.Models
{
    public class Question
    {
        public Question()
        {
            this.PresentInTests = new HashSet<Test>();
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(255,ErrorMessage ="The question must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string Content { get; set; }

        public virtual ICollection<Test> PresentInTests { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
