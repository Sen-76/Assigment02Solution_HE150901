using BusinessObject.Model;
using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CommonInterface<Product> _productService;
        public ProductController(CommonInterface<Product> productService)
        {
            _productService = productService;
        }
    }
}
