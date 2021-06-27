using reservation_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reservation_system.Data;

namespace reservation_system.Controllers 
{
    [Authorize]
    public class UserRoleManagementController : Controller
    {
        private readonly UserManager<ReservationUser> userManager;
         RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _Context;

        public UserRoleManagementController(UserManager<ReservationUser> userManager, ApplicationDbContext Context,
                                RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _Context = Context;
        }


        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Index()
        {
            //get all users and send to view
            var users = userManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Details(string userId)
        {
            //find user by userId
            //Add UserName to ViewBag
            //get userRole of users and send to view
            var user = await userManager.FindByIdAsync(userId);

            ViewBag.UserName = user.UserName;

            var userRoles = await userManager.GetRolesAsync(user);

            return View(userRoles);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DisplayRoles()
        {
            //get all roles and pass to view
            var roles = roleManager.Roles.ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            //get all users
            //get all roles
            //create selectlist and pass using viewBag
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            ViewBag.Users = new SelectList(users, "Id", "UserName");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRole userRole)
        {
            //find user from userRole.UserId
            //assign role to user
            //redirect to index

            var user = await userManager.FindByIdAsync(userRole.UserId);

            await userManager.AddToRoleAsync(user, userRole.RoleName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserRole(string role, string userName)
        {
            //get user from userName
            //remove role of user using userManager
            //return to details with parameter userId

            var user = await userManager.FindByNameAsync(userName);

            var result = await userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { userId = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string role)
        {
            //get role to delete using role Name
            //delete role using roleManager
            //redirect to displayroles

            var roleToDelete = await roleManager.FindByNameAsync(role);
            var result = await roleManager.DeleteAsync(roleToDelete);

            return RedirectToAction(nameof(DisplayRoles));
        }
    }
}
