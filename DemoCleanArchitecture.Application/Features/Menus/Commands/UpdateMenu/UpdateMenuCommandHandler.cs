using AutoMapper;
using DemoCleanArchitecture.Domain.Exceptions;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, int>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public UpdateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdWithNewsAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");
            if (menu.IsActive && !request.IsActive)
            {
                var hasActive = await _menuRepository.HasActiveNewsAsync(request.Id);
                if (hasActive)
                    throw new BusinessRuleException("Không thể ẩn menu đang có bài viết Published.");
            }
            menu.Name = request.Name;
            menu.Slug = request.Slug;
            menu.DisplayOrder = request.DisplayOrder;
            menu.IsActive = request.IsActive;
            menu.ParentId = request.ParentId;
            return await _menuRepository.UpdateAsync(request.Id, menu);
        }
    }
}
