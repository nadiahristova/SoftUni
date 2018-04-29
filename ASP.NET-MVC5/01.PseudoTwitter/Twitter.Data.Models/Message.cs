namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(400)]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }        
    }
}
