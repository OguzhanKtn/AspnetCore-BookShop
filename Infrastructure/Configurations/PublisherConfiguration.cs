using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(50)");
        builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(500)");
        builder.Property(p => p.ImageUrl).IsRequired().HasColumnType("varchar(200)");
    }
}
