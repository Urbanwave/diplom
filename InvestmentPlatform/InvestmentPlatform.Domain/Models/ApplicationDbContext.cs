using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Domain.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
          : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ImplementationStatus> ImplementationStatuses { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<SolutionType> SolutionTypes { get; set; }
        public DbSet<FavoriteSolution> FavoriteSolutions { get; set; }     
    }
}
