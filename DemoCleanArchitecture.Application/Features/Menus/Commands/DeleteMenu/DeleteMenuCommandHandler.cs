using AutoMapper;
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
                ?? throw new ArgumentNullException("Not found");
            var hasChildren = menu.Children.Any();
            if (hasChildren) throw new Exception("Menu has children");
            var hasNews = menu.News.Any();
            if (hasNews) throw new Exception("Menu has news");
            return await _menuRepository.DeleteAsync(request.Id);
        }
    }
}
