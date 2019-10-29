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

        public DbSet<Projecten3_Backend.Model.User> User { get; set; }
    }
}
