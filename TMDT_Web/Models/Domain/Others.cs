using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Others
    {
        [Key]
        public int OtherID { get; set; }
        [ForeignKey("Account")]
        public int UserID { get; set; }
        public virtual Account Account { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int? StarRating { get; set; }
        public int? ListFav { get; set; }
    }
}