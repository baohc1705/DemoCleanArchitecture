using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.CreateNews
{
    public class CreateNewsCommand : IRequest<NewsDto>
    {
       
        public int MenuId { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? ThumbnailUrl { get; set; }
        public DateTime? ScheduledAt { get; set; } 
    }
}
