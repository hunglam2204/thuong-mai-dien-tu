using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int QuantityBuy { get; set; }
        public int Price { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}