using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eSetter.Models
{

    public class SetterDbContext : DbContext
    {
        public SetterDbContext() : base("name=SetterDbContext")
        {

        }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<SubscriberHist> SubscriberHist { get; set; }
        public virtual DbSet<log> log { get; set; }
    }
}