namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Twitter.Data.UnitOfWork;

    using Twitter.Web.Models;

    public class ReportController : BaseController
    {
        public ReportController() : base(new TwitterData(new TwitterContext()))
        {
        }

        public ReportController(ITwitterData ctx) : base(ctx)
        {
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}