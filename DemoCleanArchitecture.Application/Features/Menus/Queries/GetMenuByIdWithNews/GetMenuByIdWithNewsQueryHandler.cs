using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuById;
using DemoCleanArchitecture.Domain.Exceptions;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuByIdWithNews
{
    public class GetMenuByIdWithNewsQueryHandler : IRequestHandler<GetMenuByIdWithNewsQuery, MenuDto>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenuByIdWithNewsQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<MenuDto> Handle(GetMenuByIdWithNewsQuery request, CancellationToken cancellationToken)
        {
            var data = await _menuRepository.GetByIdWithNewsAsync(request.Id)
                ?? throw new NotFoundException($"Không tìm thấy với id = {request.Id}");
            return _mapper.Map<MenuDto>(data);
        }
    }
}
