using Microsoft.EntityFrameworkCore;

namespace PDRSCalculationAPI.Entity
{
    public class PDRSDataContext : DbContext
    {
        public PDRSDataContext(DbContextOptions<PDRSDataContext> options) : base(options)
        {

        }        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Procurement> Procurements { get; set; }
    }
}
