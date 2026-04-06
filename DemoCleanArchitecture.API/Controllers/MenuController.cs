using DemoCleanArchitecture.API.Common;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Commands.CreateMenu;
using DemoCleanArchitecture.Application.Features.Menus.Commands.DeleteMenu;
using DemoCleanArchitecture.Application.Features.Menus.Queries;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuById;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuByIdWithNews;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenus;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenusWithNews;
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

        [HttpGet("GetMenusWithNews")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MenuDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenusWithNews()
        {
            var response = await _mediator.Send(new GetMenusWithNewsQuery());
            return Ok(ApiResponse<IEnumerable<MenuDto>>.Ok(response, "Get menus with news successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MenuShortDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenusById(int id)
        {
            var response = await _mediator.Send(new GetMenuByIdQuery() { Id = id});
            return Ok(ApiResponse<MenuShortDto>.Ok(response, "Get menus by id successfully"));
        }

        [HttpGet("GetMenusWithNews/{id}")]
        [ProducesResponseType(typeof(ApiResponse<MenuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenusByIdWithMenus(int id)
        {
            var response = await _mediator.Send(new GetMenuByIdWithNewsQuery() { Id = id });
            return Ok(ApiResponse<MenuDto>.Ok(response, "Get menus by id with news successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MenuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<MenuDto>.Ok(response, "Create news successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteMenuCommand() { Id = id });

                return Ok(ApiResponse<int>.Ok(response, "Delete news successfully"));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<int>.Fail("Not found"));
            }    
        }
    }
}
