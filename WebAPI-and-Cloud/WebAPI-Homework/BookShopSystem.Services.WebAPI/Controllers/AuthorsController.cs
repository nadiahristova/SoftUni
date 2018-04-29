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
    public class AuthorsController : ApiController
    {
        private BookShopEntities ctx = new BookShopEntities();

        //GET /api/authors/{id}
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var author = ctx.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return this.BadRequest("Author with Id = " + id + "does not exists.");
            }

            return this.Ok
                (
                    new AuthorViewModel
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        BookTitles = author.Books.Select(b => b.Title).ToList()
                    }
                );            
        }

        public IHttpActionResult Post(AuthorBindingModel authorModel)
        {
            if (authorModel == null || !this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            ctx.Authors.Add(new Author() 
                {
                    FirstName = authorModel.FirstName,
                    LastName = authorModel.LastName
                });

            ctx.SaveChanges();

            return this.Ok();
        }

        [EnableQuery]
        [Route("api/authors/{id}/books")]
        public IHttpActionResult GetBookInfo(int id)
        {
            var author = ctx.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return this.BadRequest("Book with Id = " + id + "does not exists.");
            }

            return this.Ok
                (
                    author.Books.Select(b => new AuthorBookViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        EditionType = b.EditionType.ToString(), 
                        Price = b.Price,
                        Copies = b.Copies,
                        ReleaseDate = b.ReleaseDate,
                        AgeRestriction = b.AgeRestriction.ToString(), 
                        Categories = b.Categories.Select(boo => boo.Name).ToList()
                    }
                ).AsQueryable()); 
        }
    }
}
