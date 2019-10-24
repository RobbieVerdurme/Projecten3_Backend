using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.ManyToManies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data
{
    public class MultimedDbContext : IdentityDbContext
    {

        #region DbSets

        public DbSet<User> Users { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ChallengeUser> ChallengeUsers { get; set; }
        public DbSet<Therapist> Therapists { get;   set; }

        #endregion

        public MultimedDbContext(DbContextOptions<MultimedDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }

        
    }
}
