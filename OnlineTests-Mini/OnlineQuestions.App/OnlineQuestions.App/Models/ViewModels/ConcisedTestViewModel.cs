namespace OnlineQuestions.App.Models.ViewModels
{
    public class ConcisedTestViewModel
    {
        public int TestId { get; set; }

        public bool IsTakenByCurrentUser { get; set; }

        public byte? CorrectAnswers { get; set; }

        public int QuestionCount { get; set; }

        public string TestTitle { get; set; }
    }
}