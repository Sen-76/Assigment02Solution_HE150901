using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
