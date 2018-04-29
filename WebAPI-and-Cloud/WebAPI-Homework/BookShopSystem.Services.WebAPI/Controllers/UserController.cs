using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Services.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace BookShopSystem.Services.WebAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {

        private BookShopEntities ctx = new BookShopEntities();

        [Authorize]
        [EnableQuery]
        [Route("api/user/{username}/purchases")]
        public IHttpActionResult Get(string username)
         {
            var wantedUser = ctx.Users.FirstOrDefault(u => u.UserName == username);

            if (wantedUser == null)
                return this.BadRequest("User " + username + " does not exist.");

            var newViewOfPurchases = wantedUser.Purchases.Select(p => new PurchaseViewModel
                {
                    Username = p.User.UserName,
                    BookPrice = p.Price,
                    Title = p.Book.Title,
                    DateOfPurchase = p.DateOfPurchase,
                    IsRecalled = p.IsRecalled
                }).OrderBy(p => p.DateOfPurchase).AsQueryable();
            
            return this.Ok(newViewOfPurchases);
        }
    }
}
