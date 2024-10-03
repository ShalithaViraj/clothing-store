using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Query;
using Clothing.Infrastructure.Data;
using Clothing.Infrastructure.Repository.Query.Common;

namespace Clothing.Infrastructure.Repository.Query
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(ClothingDBContext context)
       : base(context)
        {

        }

        public User GetUserByUserName(string email)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName.Trim() == email.Trim());

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IQueryable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            var listOfUser = _context.Users;

            return listOfUser;
        }

    }
}
