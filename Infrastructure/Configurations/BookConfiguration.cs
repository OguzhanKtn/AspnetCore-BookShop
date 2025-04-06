using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasColumnType("varchar(50)");
        builder.Property(b => b.Description).IsRequired().HasColumnType("varchar(500)");
        builder.Property(b => b.ImageUrl).IsRequired().HasColumnType("varchar(200)");
        builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(b => b.Pages).IsRequired();

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Publisher).
            WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
