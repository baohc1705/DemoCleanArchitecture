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

namespace DemoCleanArchitecture.Application.Features.News.Commands.UnpublishNews
{
    public class UnpublishNewsCommandHandler : IRequestHandler<UnpublishNewsCommand, NewsDto>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public UnpublishNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<NewsDto> Handle(UnpublishNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");

            news.Unpublish();

            await _newsRepository.UpdateAsync(news);

            return _mapper.Map<NewsDto>(news);
        }
    }
}
