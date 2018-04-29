namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Twitter.Data.UnitOfWork;

    using Twitter.Web.Models;

    public class MessageController : BaseController
    {
        public MessageController() : base(new TwitterData(new TwitterContext()))
        {
        }

        public MessageController(ITwitterData ctx) : base(ctx)
        {
        }
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
    }
}