using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AppUser> Users { get; set; }
    }

}