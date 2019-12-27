using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Web_For_Flights_Project.Controllers
{
    public class AdminLoginToken
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{UserName} {Password}";
        }
    }
}