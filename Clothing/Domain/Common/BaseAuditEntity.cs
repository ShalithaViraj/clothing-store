namespace Clothing.Domain.Common
{
    public class BaseAuditEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
