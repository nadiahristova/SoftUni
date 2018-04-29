namespace Twitter.Models
{
    using Enums;

    using System;
    using System.ComponentModel.DataAnnotations;    

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(400)]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool Read { get; set; }

        public NotificationKind NotificationKind { get; set; }

        public virtual User User { get; set; }
    }
}
