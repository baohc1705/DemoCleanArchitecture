using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.PublishNews
{
    public class PublishNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime? ScheduledAt { get; set; }
    }
}
