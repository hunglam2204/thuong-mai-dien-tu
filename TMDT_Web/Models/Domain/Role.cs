using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string setRole { get; set; }
        //------------------------------------------------------
        //------------------------------------------------------
        public ICollection<Account> account { get; set; }
    }
}