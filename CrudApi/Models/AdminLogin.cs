using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApi.Models
{
    public class AdminLogin
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}