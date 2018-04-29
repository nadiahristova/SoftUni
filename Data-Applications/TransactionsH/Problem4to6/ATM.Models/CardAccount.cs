using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CardAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string CardPIN { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal CardCash { get; set; }
    }
}
