using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Identity;
using WebApp.Repository;
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
                UsersWithRoles = await _userService.GetAllUsersWithRolesAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel viewModel)
        {
            if (User.IsInRole("admin"))
            {
                viewModel.Password = "Hejhej123";
                viewModel.ConfirmPassword = "Hejhej123";
            }

            if (ModelState.IsValid)
            {
                if (await _userService.RegisterAsync(viewModel))
                    return RedirectToAction("Login");

                ModelState.AddModelError("", "A user with the same e-mail address already exists");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string userId, string roleId)
        {
            try
            {
                var task = _userService.UpdateUsersRoleAsync(userId, roleId);
                return RedirectToAction("Employees");
            }
            catch
            {
                return View("Error", "Denied");
            }
        }
    }
}
