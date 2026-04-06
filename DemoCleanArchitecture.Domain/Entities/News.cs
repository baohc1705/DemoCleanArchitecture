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

        public Menu Menu { get; set; } = null!;
        public void Publish(DateTime? scheduledAt = null)
        {
            if (Status == NewsStatus.Archived)
                throw new Exception("Không thể đăng bài đã Archived");

            if (scheduledAt.HasValue && scheduledAt > DateTime.Now)
            {
                Status = NewsStatus.Scheduled;
                PublishedAt = scheduledAt.Value;
            }
            else
            {
                Status = NewsStatus.Published;
                PublishedAt = DateTime.UtcNow;
            }

            UpdateAt = DateTime.UtcNow;
        }

        public void Unpublish()
        {
            if (Status != NewsStatus.Published && Status != NewsStatus.Scheduled)
                throw new Exception("Chỉ có thể unpublish bài đang published hoặc scheduled");
            Status = NewsStatus.Draft;
            PublishedAt =null;
            UpdateAt = DateTime.UtcNow;
        }

        public void Archive()
        {
            if (Status != NewsStatus.Published)
                throw new Exception("Chỉ archive bài đang published");
            Status = NewsStatus.Archived;
            PublishedAt = DateTime.UtcNow;
        }

        public void UnArchive()
        {
            if (Status != NewsStatus.Archived)
                throw new Exception("Bài chưa trạng thái archived");
            Status = NewsStatus.Draft;
            UpdateAt = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            DeleteAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void MoveToMenu(int newMenuId)
        {
            if (newMenuId == MenuId)
                throw new Exception("Bài viết đã thuộc menu này");
            MenuId = newMenuId;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
