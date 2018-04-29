using System.ComponentModel.DataAnnotations;

namespace OnlineQuestions.Models
{
    public class Answer
    {
        public Answer()
        {

        }

        public Answer(string content)
        {
            this.Content = content;
        }

        public Answer(string content, bool isTheAnswer) : this(content)
        {
            this.IsTheAnswer = isTheAnswer;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(155, ErrorMessage = "The answer must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Content { get; set; }
        
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        //false by default
        public bool IsTheAnswer { get; set; }
    }
}
