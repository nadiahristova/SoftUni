using BookShopSystem.Data;
using BookShopSystem.Data.Models;
using BookShopSystem.Models;
using BookShopSystem.Services.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using Microsoft.AspNet.Identity;

namespace BookShopSystem.Services.WebAPI.Controllers
{
    public class BooksController : ApiController
    {
        private BookShopEntities ctx = new BookShopEntities();

        public IHttpActionResult Get(int id)
        {             
            var book = ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return this.BadRequest("Book with Id = " + id + "does not exists.");
            }

            return this.Ok
                (
                    new BookViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        EditionType = book.EditionType.ToString(),
                        Price = book.Price,
                        Copies = book.Copies,
                        ReleaseDate = book.ReleaseDate,
                        AgeRestriction = book.AgeRestriction.ToString(),
                        AuthorName = book.Author.FirstName == null ? 
                                        book.Author.LastName : book.Author.FirstName + " " + book.Author.LastName,
                        AuthorId = book.Author.Id,
                        Categories = book.Categories.Select(b => b.Name).ToList()
                    }
                ); 
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult SearchForBooksWithGivenSubstring(string search)
        {
            var books = ctx.Books.Where(b => b.Title.Contains(search)).OrderBy(b => b.Title).Take(10);

            return this.Ok
                (
                    books.Select(b => new 
                                        {
                                            Id = b.Id,
                                            Title = b.Title
                                        }
                ).AsQueryable()); 
        }

        public IHttpActionResult Put(int id, [FromUri]BookBindingModel newBookInfo)
        {
            if(newBookInfo == null || !this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var book = ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            book.Title = newBookInfo.Title;
            book.Price = newBookInfo.Price;
            book.Description = newBookInfo.Description;
            book.Copies = newBookInfo.Copies;           
            book.ReleaseDate = newBookInfo.ReleaseDate;
            book.AuthorId = newBookInfo.AuthorId;            
            book.EditionType = newBookInfo.EditionType != null ? (EditionType?)Enum.Parse(typeof(EditionType), newBookInfo.EditionType.Value.ToString(), true) : null;
            book.AgeRestriction = newBookInfo.AgeRestriction != null ? (AgeRestriction?)Enum.Parse(typeof(AgeRestriction), newBookInfo.AgeRestriction.Value.ToString(), true) : null;
            
            ctx.SaveChanges();
            return this.Ok(book);
        }

        public IHttpActionResult Delete(int id)
        {
            var book = ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            ctx.Books.Remove(book);
            ctx.SaveChanges();

            return this.Ok();
        }

        public IHttpActionResult Post([FromBody]BookBindingModel newBookInfo)
        {
            if(!ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var newBook = new Book()
                {
                    Title = newBookInfo.Title,
                    Price = newBookInfo.Price,
                    Description = newBookInfo.Description,
                    Copies = newBookInfo.Copies,
                    EditionType = newBookInfo.EditionType != null ?
                        (EditionType?)Enum.Parse(typeof(EditionType), newBookInfo.EditionType.Value.ToString(), true) : null,
                    AgeRestriction = newBookInfo.AgeRestriction != null ?
                        (AgeRestriction?)Enum.Parse(typeof(AgeRestriction), newBookInfo.AgeRestriction.Value.ToString(), true) : null,
                    ReleaseDate = newBookInfo.ReleaseDate,
                    Categories = newBookInfo.Categories.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => new Category() { Name = name }).ToList(),
                    AuthorId = newBookInfo.AuthorId
                };

            ctx.Books.Add(newBook);
            ctx.SaveChanges();

            return this.Ok(newBook);
        }

        [Authorize]
        [Route("api/books/buy/{id}")]
        public IHttpActionResult Put(int id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            var book = ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if(book.Copies <= 0)
            {
                return this.BadRequest("The book is sold out.");
            }

            book.Copies--;

            ctx.Users.FirstOrDefault(u => u.Id == userId)
                .Purchases.Add(new Purchase() 
                { 
                    BookId = book.Id,
                    Price = book.Price,
                    DateOfPurchase = DateTime.Now,
                    UserId = userId
                });

            ctx.SaveChanges();

            return this.Ok();
        }

        [Authorize]
        [Route("api/books/recall/{id}")]
        public IHttpActionResult PutRecall(int id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            var book = ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var purchase = ctx.Users.FirstOrDefault(u => u.Id == userId).Purchases.FirstOrDefault(p => p.BookId == id && p.IsRecalled == false);

            if (purchase == null)
                return this.BadRequest("The book has already been re-called or hasn't been purchased.");

            if ((DateTime.Now - purchase.DateOfPurchase).TotalDays > 30)
                return this.BadRequest("The book purchase occured more than 30 days ago. U don't have the right to return the item.");

            book.Copies++;
            purchase.IsRecalled = true;
            ctx.SaveChanges();

            return this.Ok();
        }
    }
}
