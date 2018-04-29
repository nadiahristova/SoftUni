using BookShopSystem.Data;
using BookShopSystem.Models;
using BookShopSystem.Services.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace BookShopSystem.Services.WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private BookShopEntities ctx = new BookShopEntities();

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var categories = ctx.Categories.Select(c => new AllCategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).AsQueryable();

            return this.Ok(categories);
        }

        public IHttpActionResult Get(int id)
        {
            var category = ctx.Categories.FirstOrDefault(c => c.Id == id);

            if(category == null)
            {
                return this.BadRequest("There is no existing record for Category with Id " + id + ".");
            }

            return this.Ok(new { Id = category.Id, Name = category.Name});
        }

        public IHttpActionResult Put(int id, string name)
        {
            if (ctx.Categories.Where(c => c.Name == name).Any())
            {
                return this.BadRequest("A Category with name " + name + "already exists.");
            }

            var category = ctx.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return this.BadRequest("There is no existing record for Category with Id " + id + ".");
            }

            category.Name = name;
            ctx.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var category = ctx.Categories.FirstOrDefault(b => b.Id == id);
            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            ctx.Categories.Remove(category);
            ctx.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Post([FromUri]string name)
        {
            if (ctx.Categories.Where(c => c.Name == name).Any())
            {
                return this.BadRequest("A Category with name " + name + " already exists.");
            }

            var category = new Category() { Name = name };

            ctx.Categories.Add(category);
            ctx.SaveChanges();

            return this.Ok();
        }
    }
}
