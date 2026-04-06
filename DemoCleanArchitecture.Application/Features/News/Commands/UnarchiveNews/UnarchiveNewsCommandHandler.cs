using AutoMapper;
using DemoCleanArchitecture.Domain.Exceptions;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.UnarchiveNews
{
    public class UnarchiveNewsCommandHandler : IRequestHandler<UnarchiveNewsCommand, int>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public UnarchiveNewsCommandHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UnarchiveNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");
            news.UnArchive();

            return await _newsRepository.UpdateAsync(news);
           
        }
    }
}
