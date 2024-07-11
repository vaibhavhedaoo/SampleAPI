using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Entities
{
    public class SampleApiDbContext : DbContext
    {
        public SampleApiDbContext() { }
        public SampleApiDbContext(DbContextOptions<SampleApiDbContext> options) :
            base(options)
        {
        }
        protected override void OnConfiguring
      (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SampleDB");
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
    }
}
