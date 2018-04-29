using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShopSystem.Data.Models
{

    public class ApplicationUser : IdentityUser
    {
        public ICollection<Purchase> books;
        public ApplicationUser()
        {
            this.books = new HashSet<Purchase>();
        }
        
        public string GetUserId()
        {
            return this.Id;
        }
        [Required]
        public decimal MoneyBalance { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
            (UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
