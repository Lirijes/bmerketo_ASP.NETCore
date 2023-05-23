using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Identity;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserService userService, UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
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
                Users = _userManager.Users.ToList(),
                Roles = _roleManager.Roles.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel model)
        {
            var pw = model.Password = "Hejhej123.";
            var cpw = model.ConfirmPassword = "Hejhej123.";

            if (ModelState.IsValid)
            {
                var user = new CustomIdentityUser 
                { 
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email, 
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, pw);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> RegisterEmployee(RegisterViewModel viewModel)
        //{
        //    if (User.IsInRole("admin"))
        //    {
        //        viewModel.Password = "Hejhej123";
        //        viewModel.ConfirmPassword = "Hejhej123";
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (await _userService.RegisterAsync(viewModel))
        //            return RedirectToAction("Login", "Account");

        //        ModelState.AddModelError("", "A user with the same e-mail address already exists");
        //    }

        //    return View(viewModel);
        //}


        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(UserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var currentRole = await _userManager.GetRolesAsync(user);

                if (currentRole.FirstOrDefault() != model.RoleId)
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRole);
                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                return RedirectToAction("Employees");
            }

            return View(model);
        }
    }
}
