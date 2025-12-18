using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Models;

namespace MyMVCApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
