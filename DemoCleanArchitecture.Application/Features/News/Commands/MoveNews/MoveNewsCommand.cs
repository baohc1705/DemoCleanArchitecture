using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.MoveNews
{
    public class MoveNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int TargetMenuId { get; set; }
    }
}
