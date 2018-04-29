using System.Collections.Generic;

namespace OnlineQuestions.App.Models.ViewModels
{
    public class AllTestsViewModel
    {
        public IEnumerable<ConcisedTestViewModel> Tests { get; set; }
    }
}