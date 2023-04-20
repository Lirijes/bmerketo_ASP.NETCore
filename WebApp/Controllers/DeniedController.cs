using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
