using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSetter.Models
{
    public class Subscriber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string prefix { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public DateTime? birthDate { get; set; }
        public string sex { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? lastLoginDate { get; set; }
        public bool consent { get; set; }
        [NotMapped]
        public string oldPass { get; set; }


        public override string ToString()
        {
            return "username: " + username + ", prefix: " + prefix + ", birthDate: + " + birthDate.ToString() + ", name: " + name + ", lastName: " + lastName + ", sex: " + sex;
        }
    }
}