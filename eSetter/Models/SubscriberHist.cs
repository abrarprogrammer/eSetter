using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSetter.Models
{
    public class SubscriberHist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string prefix { get; set; }
        public string username { get; set; }
        public DateTime createdDate { get; set; }
        public string status { get; set; }
    }
}