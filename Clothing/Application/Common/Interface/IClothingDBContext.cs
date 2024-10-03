using Clothing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clothing.Application.Common.Interface
{
    public interface IClothingDBContext
    {
        DbSet<User> Users { get; }
        DbSet<UserLoginHistory> UserLoginHistory { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task RetryOnExceptionAsync(Func<Task> func);
    }
}
