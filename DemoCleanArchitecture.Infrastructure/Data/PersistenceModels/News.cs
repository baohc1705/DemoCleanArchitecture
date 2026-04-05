using System;
using System.Collections.Generic;

namespace DemoCleanArchitecture.Infrastructure.Data.PersistenceModels;

public partial class News
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Summary { get; set; }

    public string Content { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public int? AuthorId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? PublishedAt { get; set; }

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Menu Menu { get; set; } = null!;
}
