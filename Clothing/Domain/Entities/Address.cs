namespace Clothing.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address() 
        {
            UserAddresses = new HashSet<UserAddress>();
          
        }
        public string AddressLine  { get; set; }
        public int City { get; set; }
        public int Province { get; set; }
        public int PostalCode { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
