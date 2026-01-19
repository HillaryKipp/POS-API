using AstrolPOSAPI.Application.Features.StoreType.Commands.CreateStoreType;
using AstrolPOSAPI.Application.Features.StoreType.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astrol_POS_API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreTypeDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateStoreTypeCommand command)
        {
            var storeType = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = storeType.Id }, storeType);
        }
    }
}
