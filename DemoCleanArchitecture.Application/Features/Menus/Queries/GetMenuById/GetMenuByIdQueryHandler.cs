using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuById
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuShortDto>
    {
        private readonly IMenuRepository  _menuRepository;
        private readonly IMapper _mapper;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<MenuShortDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _menuRepository.GetByIdAsync(request.Id);
            return _mapper.Map<MenuShortDto>(data);
        }
    }
}
