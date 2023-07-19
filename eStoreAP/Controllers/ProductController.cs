using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using DataAccess.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpGet("GetList")]
        public async Task<ApiResponse> GetList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.GetList();
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        [Authorize]
        [HttpGet("GetById")]
        public async Task<ApiResponse> GetById(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ApiResponse> Create(ProductVM newValue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.Create(newValue);
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Delete")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<ApiResponse> Update(ProductVM newValue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.Update(newValue);
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        [Authorize]
        [HttpGet("Search")]
        public async Task<ApiResponse> Search(string searchValue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponse()
                    {
                        Status = ResponeStatus.NotFound,
                        Message = "Model is invalid",
                        Success = false
                    };
                }
                var result = await _productService.Search(searchValue);
                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = ResponeStatus.BadRequest,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}
