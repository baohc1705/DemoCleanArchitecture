using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.DeleteNews
{
    public class DeleteNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
