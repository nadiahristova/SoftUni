using OnlineQuestions.App.Models.ViewModels;
using OnlineQuestions.Data;

using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace OnlineQuestions.App.Controllers
{
    public class HomeController : Controller
    {
        private OnlineQuestionsContext ctx = new OnlineQuestionsContext();

        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var loggedInUserId = this.User.Identity.GetUserId();

                var allTests = from t in ctx.Tests
                               where t.Questions.Any()
                               let udt = ctx.UsersDoTests
                                .Where(udt => udt.ApplicationUserId == loggedInUserId 
                                    && udt.TestId == t.Id)
                                .FirstOrDefault()
                                select new ConcisedTestViewModel
                                {
                                    TestId = t.Id,
                                    TestTitle = t.Title,
                                    QuestionCount = t.Questions.Count(),
                                    IsTakenByCurrentUser = udt != null,
                                    CorrectAnswers = udt != null ? udt.RightAnswersCount : 0
                                };

                return View(new AllTestsViewModel() { Tests = allTests });
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}