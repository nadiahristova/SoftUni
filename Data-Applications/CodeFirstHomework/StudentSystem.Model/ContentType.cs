using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    
    //[ComplexType]
    //public class ContentType
    //{
    //    public string PDF { get; set; }
    //    public string Zip { get; set; }
    //}
    public enum ContentType
    {
        PDF,
        Zip
    }
}
