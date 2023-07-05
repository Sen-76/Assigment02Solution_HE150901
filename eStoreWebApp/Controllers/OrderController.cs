using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
