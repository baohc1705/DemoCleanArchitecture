using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuByIdWithNews
{
    public class GetMenuByIdWithNewsQuery : IRequest<MenuDto>
    {
        public int Id { get; set; }
    }
}
