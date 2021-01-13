using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models
{
    public class ModelRegister
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage ="E-mail not vaid")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}