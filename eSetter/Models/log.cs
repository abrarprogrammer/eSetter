using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSetter.Models
{
    public class log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string type { get; set; }
        public string msisdn { get; set; }
        public string inputText { get; set; }
        public string errorTxt { get; set; }
        public DateTime date {get;set;}
    }
}