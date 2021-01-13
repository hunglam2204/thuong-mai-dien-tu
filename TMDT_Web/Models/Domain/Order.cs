using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime DateTimeOrder { get; set; }
        public int? Status { get; set; }
        public int? OrderPhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int TypePayment { get; set; }

        //-----------------------------------------
        //-----------------------------------------

        [ForeignKey("Account")]
        public int? UserID { get; set; }
        public virtual Account Account { get; set; }

        //-----------------------------------------
        //-----------------------------------------

        public ICollection<OrderDetail> orderDetail { get; set; }
    }
}