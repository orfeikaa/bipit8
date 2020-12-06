using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Бипит_8
{
    public class AvtoContext : DbContext
    {
        public AvtoContext() : base("DBConnection") { }

        public DbSet<Avto> Avto { get; set; }
    }
}