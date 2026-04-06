using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Queries.GetNewsById
{
    public class GetNewsByIdQuery : IRequest<NewsDto>
    {
        public int Id { get; set; }
    }
}
