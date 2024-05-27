using ClientAPI.Application.Members.Commands;
using ClientAPI.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            var query = new GetClientsQuery();
            var clients = await _mediator.Send(query);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _mediator.Send(new GetClientByIdQuery { Id = id });
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
        {
            var client = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, UpdateClientCommand command)
        {
            command.Id = id;

            var client = await _mediator.Send(command);
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _mediator.Send(new DeleteClientCommand { Id = id });
            return NoContent();
        }
    }
}
