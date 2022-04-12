using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationAndAuthorization.Models
{
    public class UserLoginModel
    {
        public string  UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int IsValid { get; set; }
        public string LoginErrorMessage { get; set; }


    }
}