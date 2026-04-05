using System;
using System.Collections.Generic;

namespace DemoCleanArchitecture.Infrastructure.Data.PersistenceModels;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int? ParentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual Menu? Parent { get; set; }
}
