

using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Query.Common;

namespace Clothing.Domain.Repository.Query
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        User GetUserByUserName(string userName);

        Task<IQueryable<User>> GetAllUsers(CancellationToken cancellationToken);
    }
}
