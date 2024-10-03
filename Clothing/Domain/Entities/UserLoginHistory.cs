
using Clothing.Domain.Common;

namespace Clothing.Domain.Entities
{
    public class UserLoginHistory : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public virtual User User { get; set; }
    }
}
