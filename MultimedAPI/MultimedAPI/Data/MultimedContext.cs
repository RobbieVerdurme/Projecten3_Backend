using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data
{
    public class MultimedDbContext : IdentityDbContext
    {

        public MultimedDbContext(DbContextOptions<MultimedDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
    }
}
