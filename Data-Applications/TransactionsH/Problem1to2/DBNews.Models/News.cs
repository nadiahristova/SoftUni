using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBNews.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string NewsContent { get; set; }
    }
}
