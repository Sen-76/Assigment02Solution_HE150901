using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class OrderUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
