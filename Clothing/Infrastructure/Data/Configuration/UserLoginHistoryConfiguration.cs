using Clothing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothing.Infrastructure.Data.Configuration
{
    public class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("UserLoginHistory");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne<User>(f => f.User)
               .WithMany(f => f.LogedUser)
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
