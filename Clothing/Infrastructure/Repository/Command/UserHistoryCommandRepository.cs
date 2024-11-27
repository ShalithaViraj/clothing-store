using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Command;
using Clothing.Infrastructure.Data;
using Clothing.Infrastructure.Repository.Command.Common;

namespace Clothing.Infrastructure.Repository.Command
{
    public class UserHistoryCommandRepository: CommandRepository<UserLoginHistory>, IUserHistoryCommandRepository
    {
        public UserHistoryCommandRepository(ClothingDBContext dbContext) : base(dbContext) { }
    }
}
