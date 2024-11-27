using Clothing.Domain.Enum;
using Clothing.Domain.Common;

namespace Clothing.Domain.Entities
{
    public class User : BaseEntity
    {
        public User() 
        {
            LogedUser = new HashSet<UserLoginHistory>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNo { get; set; }
        public Position Position { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        public virtual ICollection<UserLoginHistory> LogedUser { get; set; }


    }
}
