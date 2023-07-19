using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class ProductUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
