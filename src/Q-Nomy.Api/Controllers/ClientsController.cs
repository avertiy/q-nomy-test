﻿using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QNomy.Application.Clients.Command.AddClient;
using QNomy.Application.Clients.Command.ProcessNext;
using QNomy.Application.Clients.Query.GetClients;

namespace QNomy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetClientsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetClientsQuery());
            if (result is not { HasData: true })
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("next")]
        [ProducesResponseType(typeof(ProcessNextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessNext()
        {
            var result = await _mediator.Send(new ProcessNextCommand());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            var response = await _mediator.Send(new AddClientCommand() { Name = name});
            var result = new ObjectResult(response);
            if (response?.Success == true)
            {
                result.StatusCode = StatusCodes.Status201Created;
            }

            return result;
        }
    }
}
