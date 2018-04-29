namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Web.Models;

    using Data;
    using Twitter.Data.UnitOfWork;
    using System.Linq;

    public class TweetController : BaseController
    {
        public TweetController() : base(new TwitterData(new TwitterContext()))
        {
        }

        public TweetController(ITwitterData ctx) : base(ctx)
        {
        }

        // GET: Tweet
        public ActionResult Index()
        {
            var allUsers = Data.Users.All().ToList();
            return this.View(allUsers);
        }
    }
}