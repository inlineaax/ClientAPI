using ClientAPI.Domain.Entities;
using ClientAPI.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClientConfiguration());
        }
    }
}
