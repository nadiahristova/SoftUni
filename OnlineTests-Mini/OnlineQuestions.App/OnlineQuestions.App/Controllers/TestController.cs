using Microsoft.AspNet.Identity;
using OnlineQuestions.App.Models.BindingModels;
using OnlineQuestions.App.Models.ViewModels;
using OnlineQuestions.Data;
using OnlineQuestions.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineQuestions.App.Controllers
{
    public class TestController : Controller
    {
        private const int PageSize = 1;
        private OnlineQuestionsContext ctx = new OnlineQuestionsContext();

        [Authorize]
        [HttpGet]
        public ActionResult GetTestQuestions(int testId, int? page)
        {
            var test = ctx.Tests.FirstOrDefault(t => t.Id == testId);
            if (test == null)
            {
                return HttpNotFound("Test not found");
            }

            return View("Index", new TestViewModel()
            {
                TestId = test.Id,
                TestTitle = test.Title,
                QuestionIds = test.Questions.Select(q => q.Id).ToArray(),
                Questions = test.Questions.ToPagedList(page ?? 1, PageSize)
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitTest(int testId, ICollection<QuestionAnswers> answers)
        {
            var userId = this.User.Identity.GetUserId();
            var test = ctx.Tests.FirstOrDefault(t => t.Id == testId);
            if (test == null)
            {
                return HttpNotFound("Test not found");
            }

            var testEntry = new UserDoesTest()
            {
                User = ctx.Users.FirstOrDefault(u => u.Id == userId),
                Test = test,
                RightAnswersCount = (byte)(from question in test.Questions 
                                    join answerInfo in answers on question.Id equals answerInfo.QuestionId 
                                    let rightAnswerId = question.Answers.FirstOrDefault(a => a.IsTheAnswer).Id
                                    select answerInfo.AnswerId == rightAnswerId ? 1 : 0).Sum()
            };

            test.UserDoesTest.Add(testEntry);
            ctx.SaveChanges();

            return Json(new { url = @"/Home/Index" });
        }
    }
}