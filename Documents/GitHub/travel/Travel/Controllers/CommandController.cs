using Travel.Models;
using Travel.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : Controller
    {
        private readonly ICommandService _commandService;
        public CommandController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet("List Commands")]
        public async Task<IEnumerable<Command>> GetCommands()
        {
            return await _commandService.GetAll();
        }

        [HttpGet("Command by Id")]
        public async Task<ActionResult<Command>> GetCommand(int id)
        {
            return await _commandService.GetById(id);
        }

        [HttpPost("Add Command")]
        public async Task<ActionResult<Command>> PostCommand([FromBody] Command command)
        {
            var newCommand = await _commandService.Create(command);
            return CreatedAtAction(nameof(GetCommands), new { id = newCommand.CommandID }, newCommand);
        }

        [HttpPut("Update Command")]
        public async Task<ActionResult> PutCommand(int id, [FromBody] Command command)
        {
            if (id != command.CommandID)
            {
                return BadRequest();
            }

            await _commandService.Update(command);

            return NoContent();
        }

        [HttpDelete("Delete Command")]
        public async Task<ActionResult> Delete(int id)
        {
            var commandToDelete = await _commandService.GetById(id);
            if (commandToDelete == null)
                return NotFound();

            await _commandService.Delete(commandToDelete.CommandID);
            return NoContent();
        }

    }
}
