using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Employees()
        {
            var viewModel = new UsersIndexViewModel
            {
                UsersWithRoles = await _userService.GetAllUsersWithRolesAsync(),
                //Roles = await _userService.UpdateRoleOnEmployeeAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> RegisterEmployee(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.RegisterEmployeeAsync(viewModel))
                    return RedirectToAction("Employees");

                ModelState.AddModelError("", "A user with the same e-mail address already exists");
            }
            return View(viewModel);
        }
    }
}
