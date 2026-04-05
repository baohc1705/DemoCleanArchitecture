using DemoCleanArchitecture.API.Common;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ApiControllerBase
    {
        public MenuController(ISender mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MenuShortDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenus()
        {
            var response = await _mediator.Send(new GetMenusQuery());
            return Ok(ApiResponse<IEnumerable<MenuShortDto>>.Ok(response, "Get menus successfully"));
        }
    }
}
