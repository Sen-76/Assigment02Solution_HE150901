using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
