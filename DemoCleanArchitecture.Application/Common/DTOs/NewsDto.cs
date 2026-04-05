using DemoCleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Common.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public NewsStatus Status { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewCount { get; set; }
        public MenuShortDto MenuShort { get; set; } = new MenuShortDto();
    }

    public class NewsShortDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public NewsStatus Status { get; set; }
        public DateTime? PublishedAt { get; set; }
    } 
}
