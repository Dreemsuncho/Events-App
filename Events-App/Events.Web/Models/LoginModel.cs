using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Models
{
    public class LoginModel
    {
        public string LoginEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
