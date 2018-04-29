namespace Twitter.Web.Controllers
{
    using Twitter.Web.Models;

    using System.Web.Mvc;
    using Twitter.Data.UnitOfWork;
    using Data;

    public class HomeController : BaseController
    {

        public HomeController() : base(new TwitterData(new TwitterContext()))
        {
        }

        public HomeController(ITwitterData ctx) : base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}