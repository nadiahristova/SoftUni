using System;
using System.Collections.Generic;

namespace BookShopSystem.Services.WebAPI.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<String> BookTitles { get; set; }
    }

    public class AuthorBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EditionType { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string AgeRestriction { get; set; }
        public ICollection<String> Categories { get; set; }
    }

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EditionType { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string AgeRestriction { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public ICollection<String> Categories { get; set; }
    }

    public class AllCategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PurchaseViewModel
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public decimal BookPrice { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public bool IsRecalled { get; set; }
    }
}
