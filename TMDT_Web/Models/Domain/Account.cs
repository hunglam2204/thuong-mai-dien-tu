using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail not vaid")]
        [Required(ErrorMessage = "Type Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Type Your Password")]
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int? Points { get; set; }
        public int Status { get; set; }
        //------------------------------------------------------
        //------------------------------------------------------
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
        //------------------------------------------------------
        //------------------------------------------------------
        public ICollection<Order> order { get; set; }
        public ICollection<Review> review { get; set; }
        public ICollection<Others> others { get; set; }
    }
}