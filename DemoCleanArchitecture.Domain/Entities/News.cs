using DemoCleanArchitecture.Domain.Enums;

namespace DemoCleanArchitecture.Domain.Entities
{
    public class News : BaseEntity
    {
        public int MenuId { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public NewsStatus Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewCount { get; set; }

    }
}
