using System.Web.Mvc;
using Twitter.Data.UnitOfWork;

namespace Twitter.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ITwitterData data;

        protected BaseController(ITwitterData data)
        {
            this.data = data;
        }

        protected ITwitterData Data { get { return this.data; } }
    }
}