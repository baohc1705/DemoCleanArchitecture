using DemoCleanArchitecture.API.Common;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Commands.CreateMenu;
using DemoCleanArchitecture.Application.Features.Menus.Commands.DeleteMenu;
using DemoCleanArchitecture.Application.Features.Menus.Commands.UpdateMenu;
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
    public class MenuController : ControllerBase
    {
        private readonly ISender _mediator;
        public MenuController(ISender mediator)
        {
            _mediator = mediator; // inject dependency mediator
        }

        // Get thông tin tất cả record của menu
        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var response = await _mediator.Send(new GetMenusQuery());
            return Ok(ApiResponse<IEnumerable<MenuShortDto>>.Ok(response, "Get menus successfully",StatusCodes.Status200OK));
        }

        // Get thông tin tất cả record của menu và cùng với thông tin news
        [HttpGet("GetMenusWithNews")]
        public async Task<IActionResult> GetMenusWithNews()
        {
            var response = await _mediator.Send(new GetMenusWithNewsQuery());
            return Ok(ApiResponse<IEnumerable<MenuWithNewsDto>>.Ok(response, "Get menus with news successfully", StatusCodes.Status200OK));
        }

        // Get thông tin tất cả record của menu và cùng với thông tin news
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenusById(int id)
        {
            var response = await _mediator.Send(new GetMenuByIdQuery() { Id = id });
            return Ok(ApiResponse<MenuShortDto>.Ok(response, "Get menus by id successfully", StatusCodes.Status200OK));
        }

        [HttpGet("GetMenusWithNews/{id}")]
        public async Task<IActionResult> GetMenusByIdWithMenus(int id)
        {
            var response = await _mediator.Send(new GetMenuByIdWithNewsQuery() { Id = id });
            return Ok(ApiResponse<MenuDto>.Ok(response, "Get menus by id with news successfully", StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Ok(response, "Create menu successfully", StatusCodes.Status201Created));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {

            var response = await _mediator.Send(new DeleteMenuCommand() { Id = id });

            return Ok(ApiResponse<int>.Ok(response, "Delete menu successfully", StatusCodes.Status204NoContent));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, UpdateMenuCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<int>.Fail("Id not same"));

            var response = await _mediator.Send(command);

            return Ok(ApiResponse<int>.Ok(response, "Update menu successfully", StatusCodes.Status200OK));

        }
    }
}
