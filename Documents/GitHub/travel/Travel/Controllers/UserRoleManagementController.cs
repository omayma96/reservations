using reservation_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Travel.Models;
using Travel.Services;
using System.Collections.Generic;

namespace Travel.Controllers 
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
         RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _Context;
        private readonly IUserRoleManagementService _userRoleManagementService;

        public UserRoleManagementController(UserManager<ApplicationUser> userManager, ApplicationDbContext Context,
                                RoleManager<IdentityRole> roleManager, IUserRoleManagementService userRoleManagementService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _Context = Context;
            _userRoleManagementService = userRoleManagementService;
        }
        


       [HttpGet("List Roles")]
        public async Task<IEnumerable<UserRole>> GetRoles()
        {
            return await _userRoleManagementService.GetAll();
        }

        [HttpGet("Role by Id")]
        public async Task<ActionResult<UserRole>> GetRole(int id)
        {
            return await _userRoleManagementService.FetchById(id);
        }

        [HttpDelete("Delete Role")]
        public async Task<ActionResult> Delete(int id)
        {
            var roleToDelete = await _userRoleManagementService.FetchById(id);
            if (roleToDelete == null)
                return NotFound();

            await _userRoleManagementService.RemoveRole(roleToDelete.RoleName);
            return NoContent();
        }

        [HttpPost("Add Role")]
        public async Task<ActionResult<UserRole>> PostProduct([FromBody] UserRole userRole)
        {
            var newProduct = await _userRoleManagementService.Create(userRole);
            return CreatedAtAction(nameof(GetRoles), new { Name = newProduct.RoleName }, newProduct);
        }


    }
}
