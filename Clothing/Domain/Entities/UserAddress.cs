namespace Clothing.Domain.Entities
{
    public class UserAddress 
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public bool IsPrimary { get; set; }

        public virtual User User {  get; set; }
        public virtual Address Address { get; set; }

    }
}
