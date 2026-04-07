using DemoCleanArchitecture.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenusWithNews
{
    public class GetMenusWithNewsQuery : IRequest<IEnumerable<MenuWithNewsDto>>
    {
    }
}
