using Clothing.Domain.Enum;
using Clothing.Domain.Common;

namespace Clothing.Domain.Entities
{
    public class User : BaseAuditEntity
    {
        public User() 
        {
            CreatedUsers = new HashSet<User>();
            UpdatedUsers = new HashSet<User>();
            LogedUser = new HashSet<UserLoginHistory>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNo { get; set; }
        public Position Position { get; set; }

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> UpdatedUsers { get; set; }

        public virtual ICollection<UserLoginHistory> LogedUser { get; set; }


    }
}
