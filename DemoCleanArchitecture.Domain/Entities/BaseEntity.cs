namespace DemoCleanArchitecture.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeleteAt { get; set; }
        public bool IsDeleted => DeleteAt.HasValue;
    }
}
