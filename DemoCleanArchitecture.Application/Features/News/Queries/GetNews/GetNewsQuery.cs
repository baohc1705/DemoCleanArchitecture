using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Queries.GetNews
{
    public class GetNewsQuery : IRequest<IEnumerable<NewsShortDto>>
    {

    }
}
