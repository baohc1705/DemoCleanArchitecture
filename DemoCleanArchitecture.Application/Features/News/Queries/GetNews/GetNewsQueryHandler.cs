using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Queries.GetNews
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, IEnumerable<NewsShortDto>>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public GetNewsQueryHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NewsShortDto>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
        {
            var data = await _newsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NewsShortDto>>(data);
        }
    }
}
