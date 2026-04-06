using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
