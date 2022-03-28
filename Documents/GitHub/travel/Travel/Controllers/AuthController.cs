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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);       

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }


        [HttpGet("ListUsers")]
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _authService.GetUsers();
        }

        [HttpGet("GetUserByID")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            return await _authService.GetUser(id);
        }

        [HttpPut("EditUser")]
        public async Task<ActionResult> PutUser(string id, [FromBody] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _authService.EditUser(user);

            return NoContent();
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var UserToDelete = await _authService.GetUser(id);
            if (UserToDelete == null)
                return NotFound();

            await _authService.DeleteUser(UserToDelete.Id);
            return NoContent();
        }
    }
}
