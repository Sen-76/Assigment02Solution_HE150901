using Microsoft.AspNetCore.Mvc;

namespace eStoreWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
