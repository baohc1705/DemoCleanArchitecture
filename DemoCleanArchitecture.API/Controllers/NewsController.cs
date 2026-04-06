using DemoCleanArchitecture.API.Common;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Features.Menus.Queries.GetMenuByIdWithNews;
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
    }
}
