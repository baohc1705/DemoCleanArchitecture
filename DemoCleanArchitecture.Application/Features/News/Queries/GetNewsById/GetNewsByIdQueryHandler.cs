using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Exceptions;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Queries.GetNewsById
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsDto>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public GetNewsByIdQueryHandler (INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<NewsDto> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");
            await _newsRepository.IncrementViewCountAsync(request.Id);
            return _mapper.Map<NewsDto>(data);
        }
    }
}
