using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.UnarchiveNews
{
    public class UnarchiveNewsCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
