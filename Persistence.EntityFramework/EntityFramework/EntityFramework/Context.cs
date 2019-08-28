using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Root>().HasKey(x => x.Key);
        }
    }
}
