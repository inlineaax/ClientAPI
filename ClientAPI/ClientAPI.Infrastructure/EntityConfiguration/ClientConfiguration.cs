using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientAPI.Domain.Entities;

namespace ClientAPI.Infrastructure.EntityConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Gender).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(150).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
        }        
    }
}
