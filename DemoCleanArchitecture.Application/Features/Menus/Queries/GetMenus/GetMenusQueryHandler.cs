using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenus
{
    public class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuShortDto>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenusQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MenuShortDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
        {
            var query = await _menuRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuShortDto>>(query);
        }
    }
}
