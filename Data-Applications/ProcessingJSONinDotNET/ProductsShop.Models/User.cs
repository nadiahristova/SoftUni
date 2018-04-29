using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductsShop.Models
{
    public class User
    {
        private ICollection<User> friends;
        private ICollection<Product> productsBought;
        private ICollection<Product> productsSold;
        public User()
        {
            this.friends = new HashSet<User>();
            this.productsBought = new HashSet<Product>();
            this.productsSold = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]        
        public string LastName { get; set; }
        public Nullable<int> Age { get; set; }
        public virtual ICollection<User> Friends 
        {
            get { return this.friends; }
            set { this.friends = value; } 
        }

        [InverseProperty("Buyer")]
        public virtual ICollection<Product> ProductsBought
        {
            get { return this.productsBought; }
            set { this.productsBought = value; }
        }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> ProductsSold
        {
            get { return this.productsSold; }
            set { this.productsSold = value; }
        }
    }
}
