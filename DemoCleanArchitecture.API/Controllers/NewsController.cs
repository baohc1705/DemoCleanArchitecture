using Azure;
using DemoCleanArchitecture.API.Common;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Commands.CreateMenu;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuByIdWithNews;
using DemoCleanArchitecture.Application.Features.News.Commands.ArchiveNews;
using DemoCleanArchitecture.Application.Features.News.Commands.CreateNews;
using DemoCleanArchitecture.Application.Features.News.Commands.DeleteNews;
using DemoCleanArchitecture.Application.Features.News.Commands.MoveNews;
using DemoCleanArchitecture.Application.Features.News.Commands.PublishNews;
using DemoCleanArchitecture.Application.Features.News.Commands.UnarchiveNews;
using DemoCleanArchitecture.Application.Features.News.Commands.UnpublishNews;
using DemoCleanArchitecture.Application.Features.News.Commands.UpdateNews;
using DemoCleanArchitecture.Application.Features.News.Queries.GetNews;
using DemoCleanArchitecture.Application.Features.News.Queries.GetNewsById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ApiControllerBase
    {
        public NewsController(ISender mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<NewsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var response = await _mediator.Send(new GetNewsByIdQuery() { Id = id });
            return Ok(ApiResponse<NewsDto>.Ok(response, "Get news by id successfully"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NewsShortDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNews()
        {
            var response = await _mediator.Send(new GetNewsQuery());
            return Ok(ApiResponse<IEnumerable<NewsShortDto>>.Ok(response, "Get news by id successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<NewsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateNews([FromBody] CreateNewsCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<NewsDto>.Ok(response, "Create menu successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateNews(int id, [FromBody] UpdateNewsCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Ok(response, "Update menu successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SoftDeleteNews(int id)
        {

            var response = await _mediator.Send(new DeleteNewsCommand { Id = id });
            if (response < 1)
                return BadRequest(ApiResponse<int>.Fail());
            return Ok(ApiResponse<int>.Ok(response, "Update menu successfully"));
        }

        [HttpPatch("{id}/publish")]
        [ProducesResponseType(typeof(ApiResponse<NewsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> PublishNews(int id, PublishNewsCommand command)
        {
            var res = await _mediator.Send(new PublishNewsCommand { Id = id });
            var msg = command.ScheduledAt.HasValue == true
                ? $"Đã lên lịch đăng bài vào {command.ScheduledAt:dd/MM/yyyy}."
                : "Bài viết đã được publish";
            return Ok(ApiResponse<NewsDto>.Ok(res, msg));
        }

        [HttpPatch("{id}/unpublish")]
        [ProducesResponseType(typeof(ApiResponse<NewsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UnpublishNews(int id)
        {
            var res = await _mediator.Send(new UnpublishNewsCommand { Id = id});
            return Ok(ApiResponse<NewsDto>.Ok(res, "Bài viết đã được chuyển về Draft."));
        }

        [HttpPatch("{id}/move")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MoveToMenu(int id, MoveNewsCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Ok(res, $"Bài viết đã được chuyển về menu={command.TargetMenuId}"));
        }

        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ArchiveNews(int id)
        {
            var res = await _mediator.Send(new ArchiveNewsCommand() { Id = id});
            return Ok(ApiResponse<int>.Ok(res, $"Bài viết đã được chuyển về Archive"));
        }

        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UnarchiveNews(int id)
        {
            var res = await _mediator.Send(new UnarchiveNewsCommand() { Id = id });
            return Ok(ApiResponse<int>.Ok(res, $"Bài viết đã được chuyển về draft"));
        }
    }
}
