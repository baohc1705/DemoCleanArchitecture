using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ISender _mediator;
        protected ApiControllerBase(ISender mediator) {
            _mediator = mediator;
        }

    }
}
