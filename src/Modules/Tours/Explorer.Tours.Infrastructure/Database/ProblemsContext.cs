using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database
{
    public class ProblemsContext : DbContext
    {

        public DbSet<Problem> Problem { get; set; }

        public DbSet<Problem> Problems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.HasDefaultSchema("tours");
            modelBuilder.HasDefaultSchema("problems");
        }
    }
}
