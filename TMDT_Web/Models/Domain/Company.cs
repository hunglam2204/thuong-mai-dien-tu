using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [Required(ErrorMessage ="Input Company Name")]
        public string CompanyName { get; set; }

        public ICollection<Brand> brand { get; set; }
    }
}