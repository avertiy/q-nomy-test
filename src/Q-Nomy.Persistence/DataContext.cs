using Microsoft.EntityFrameworkCore;
using QNomy.Domain.Entities;

namespace QNomy.Api.Data
{
    public class DataContext : DbContext
    {
	    private const string Schema = "dbo";

        protected DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var client = modelBuilder.Entity<Client>();
            client.ToTable("Clients", Schema);
        }

        public DbSet<Client> Clients { get; set; }
    }
}
