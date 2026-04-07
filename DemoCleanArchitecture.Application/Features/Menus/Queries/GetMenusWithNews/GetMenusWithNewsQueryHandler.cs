using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenusWithNews
{
    internal class GetMenusWithNewsQueryHandler : IRequestHandler<GetMenusWithNewsQuery, IEnumerable<MenuWithNewsDto>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenusWithNewsQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MenuWithNewsDto>> Handle(GetMenusWithNewsQuery request, CancellationToken cancellationToken)
        {
            var data = await _menuRepository.GetAllWithNewsAsync();
            return _mapper.Map<IEnumerable<MenuWithNewsDto>>(data);
        }
    }
}
