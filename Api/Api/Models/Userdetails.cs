using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Userdetails
    {
        public int? candidateid { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int challengessolved { get; set; }
        public int expertlevel { get; set; }
        public int? ds { get; set; }
        public int? algorithm { get; set; }
        public int? c { get; set; }
        public int? java { get; set; }
        public int? phyton { get; set; }
        public int? votecount { get; set; }
        public bool? voted { get; set; }
        public string role { get; set; }
    }
}