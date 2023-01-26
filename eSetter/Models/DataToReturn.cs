using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSetter.Models
{
    public class DataToReturn
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; } = new List<dynamic>();
    }
}