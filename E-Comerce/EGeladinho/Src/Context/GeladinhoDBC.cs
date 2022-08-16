using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Context
{
    public class GeladinhoDBC : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public GeladinhoDBC(DbContextOptions<GeladinhoDBC> options) : base(options)
        {
        }
    }
}