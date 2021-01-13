using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public ICollection<Product> product { get; set; }
    }
}