
using Clothing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothing.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email)
                .IsRequired(false);


            builder.HasOne<User>(f => f.CreatedByUser)
               .WithMany(f => f.CreatedUsers)
               .HasForeignKey(f => f.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

            builder.HasOne<User>(f => f.UpdatedByUser)
                .WithMany(f => f.UpdatedUsers)
                .HasForeignKey(f => f.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

        }
    }
}
