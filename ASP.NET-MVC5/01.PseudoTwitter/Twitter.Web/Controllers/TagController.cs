namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Twitter.Data.UnitOfWork;

    using Twitter.Web.Models;

    public class TagController : BaseController
    {
        public TagController() : base(new TwitterData(new TwitterContext()))
        {
        }

        public TagController(ITwitterData ctx) : base(ctx)
        {
        }
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }
    }
}