using reservation_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reservation_system.Migrations;

namespace reservation_system.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly UserManager<ReservationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<ReservationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        [Authorize(Roles = " Admin")]

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }


        [Authorize(Roles = " Admin")]

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [Authorize(Roles = " Admin")]


        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }




        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        private void Errors(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
