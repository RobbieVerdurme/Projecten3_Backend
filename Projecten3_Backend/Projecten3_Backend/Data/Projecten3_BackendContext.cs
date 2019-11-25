using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;

namespace Projecten3_Backend.Models
{
    public class Projecten3_BackendContext : IdentityDbContext
    {
        public Projecten3_BackendContext(DbContextOptions<Projecten3_BackendContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Therapist> Therapist { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeUser> ChallengeUser { get; set; }
        public DbSet<TherapistUser> TherapistUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TherapistType> TherapistType { get; set; }
        public DbSet<OpeningTimes> OpeningTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
