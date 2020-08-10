using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Vote
    {
        public int whovoted { get; set; }
        public int votedfor { get; set; }
    }
}