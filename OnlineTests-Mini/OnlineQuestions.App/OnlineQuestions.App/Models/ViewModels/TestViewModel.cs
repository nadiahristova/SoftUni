using OnlineQuestions.Models;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuestions.App.Models.ViewModels
{
    public class TestViewModel
    {
        public int TestId { get; set; }

        public string TestTitle { get; set; }

        public int[] QuestionIds { get; set; }

        [UIHint("QuestionTemplate")]
        public IPagedList<Question> Questions { get; set; }
    }
}