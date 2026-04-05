using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuById
{
    public class GetMenuByIdQuery : IRequest<MenuShortDto> 
    {
        public int Id { get; set; }
    }
}
