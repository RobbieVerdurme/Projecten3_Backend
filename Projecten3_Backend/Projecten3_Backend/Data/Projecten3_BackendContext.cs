using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Model;

namespace Projecten3_Backend.Models
{
    public class Projecten3_BackendContext : DbContext
    {
        public Projecten3_BackendContext (DbContextOptions<Projecten3_BackendContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Therapist> Therapist { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
    }
}
