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
    public class UnpublishNewsCommandHandler : IRequestHandler<UnpublishNewsCommand, int>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public UnpublishNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UnpublishNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");

            news.Unpublish();

            var data = await _newsRepository.UpdateAsync(news);

            return data > 0 ? news.Id : 0;
        }
    }
}
