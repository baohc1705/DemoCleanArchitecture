using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, MenuDto>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public CreateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
           
        }
        public async Task<MenuDto> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            

            var menu = new Menu
            {
                Name = request.Name,
                Slug = request.Slug,
                DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                ParentId = request.ParentId,
            };

            var createdData = await _menuRepository.CreateAsync(menu);
            return _mapper.Map<MenuDto>(createdData);
        }
    }
}
