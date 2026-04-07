using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.CreateNews
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, int>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public CreateNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = new Domain.Entities.News
            {
                MenuId = request.MenuId,
                Title = request.Title,
                Slug = request.Slug,
                Summary = request.Summary,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                Status = Domain.Enums.NewsStatus.Draft,
            };

            if (request.ScheduledAt.HasValue)
                news.Publish(request.ScheduledAt.Value);
            var createdData = await _newsRepository.CreateAsync(news);
            return createdData != null ? createdData.Id : 0;
        }
    }
}
