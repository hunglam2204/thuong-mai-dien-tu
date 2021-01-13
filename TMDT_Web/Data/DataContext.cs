    using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Data
{
    public class DataContext: DbContext
    {
        public DataContext():base("DataContext")
        { 
        }

        public DbSet<Product> product { get; set; }
        public DbSet<Brand> brand { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<Review> review { get; set; }
    }
}