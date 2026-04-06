using AutoMapper;
using DemoCleanArchitecture.Domain.Exceptions;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, int>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdWithNewsAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");

            var hasChildren = menu.Children.Any();
            if (hasChildren) 
                throw new BusinessRuleException("Menu còn menu con. Xóa hoặc di chuyển các menu con trước.");

            var hasNews = menu.News.Any();
            if (hasNews) 
                throw new BusinessRuleException("Menu vẫn còn bài viết. Di chuyển hoặc xóa tất cả bài trước.");

            return await _menuRepository.DeleteAsync(request.Id);
        }
    }
}
