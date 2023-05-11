using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Contexts;
using WebApp.Models.Identity;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public AdminController(UserService userService, IdentityContext identityContext, UserManager<CustomIdentityUser> userManager)
        {
            _userService = userService;
            _identityContext = identityContext;
            _userManager = userManager;
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
    }
}
