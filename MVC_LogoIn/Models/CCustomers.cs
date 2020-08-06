using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_LogoIn.Models
{
    public class CCustomers
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }

    }
}