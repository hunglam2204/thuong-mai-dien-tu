using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public int? size { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? View { get; set; }
        public string Image { get; set; }

        //Khóa ngoại
        [ForeignKey("Brand")]
        public int BrandID { get; set; }
        public virtual Brand Brand { get; set; }



        public ICollection<OrderDetail> orderDetail { get; set; }
        public ICollection<Review> review { get; set; }
        public ICollection<Others> others { get; set; }
    }
}