using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.PublishNews
{
    public class PublishNewsCommandHandler : IRequestHandler<PublishNewsCommand, NewsDto>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public PublishNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<NewsDto> Handle(PublishNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new Exception("Not found");
            
            news.Publish(request.ScheduledAt);

            await _newsRepository.UpdateAsync(news);

            return _mapper.Map<NewsDto>(news);

        }
    }
}
