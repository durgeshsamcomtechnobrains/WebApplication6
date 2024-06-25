using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class authController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
