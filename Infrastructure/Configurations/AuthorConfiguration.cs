using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasColumnType("varchar(50)");
        builder.Property(a => a.SurName).IsRequired().HasColumnType("varchar(50)");
        builder.Property(a => a.Authobiography).IsRequired().HasColumnType("varchar(500)");
        builder.Property(a => a.ImageUrl).IsRequired().HasColumnType("varchar(200)");
    }
}
