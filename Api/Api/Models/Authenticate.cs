using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Authenticate
    {
        public string username { get; set; }
        public string password { get; set; }
        public int? userid { get; set; }
    }
}