using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Models
{
    public class Category
    {
        public ICollection<Book> books;

        public Category()
        {
            this.books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books
        {
            get { return this.books; }
            set { this.books = value; }
        }
    }
}
