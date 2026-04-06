using AutoMapper;
using DemoCleanArchitecture.Domain.Enums;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public UpdateNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new Exception("Not found");
            if (news.Status == NewsStatus.Archived)
                throw new Exception("Khong sua bai archived");
            news.Title = request.Title;
            news.Summary = request.Summary;
            news.Content = request.Content;
            news.ThumbnailUrl = request.ThumbnailUrl;
            news.UpdateAt = DateTime.UtcNow;

            return await _newsRepository.UpdateAsync(news);
        }
    }
}
