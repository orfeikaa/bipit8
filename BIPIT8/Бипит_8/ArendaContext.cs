using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Бипит_8
{
    public class ArendaContext : DbContext
    {
        public ArendaContext() : base("DBConnection") { }

        public DbSet<Arenda> Arenda { get; set; }
    }
}