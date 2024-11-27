using Clothing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothing.Infrastructure.Data.Configuration
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder) 
        {
            builder.ToTable("UserAddress");

            builder.HasKey(x => new { x.UserId, x.AddressId });

            builder.HasOne<Address>(a => a.Address)
                   .WithMany(at => at.UserAddresses)
                   .HasForeignKey(a => a.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(a => a.User)
                   .WithMany(at => at.UserAddresses)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
       
    }
}
