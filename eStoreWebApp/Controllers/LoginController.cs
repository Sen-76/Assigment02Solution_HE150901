using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
