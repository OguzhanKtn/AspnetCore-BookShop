using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.City).HasColumnType("varchar(50)");
        builder.Property(x => x.Town).HasColumnType("varchar(20)");
        builder.Property(x => x.Country).HasColumnType("varchar(50)");
        builder.Property(x => x.FullAddress).HasColumnType("varchar(250)");

        builder.HasOne(x => x.AppUser)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

