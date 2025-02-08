using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace A1Database.Data
{
    public class A1DbContext : DbContext
    {
        public A1DbContext(DbContextOptions<A1DbContext> options) : base(options) {}
        public DbSet<Models.Sign> Signs { get; set; }
        public DbSet<Models.Comment> Comments{get;set;}
    }
}
