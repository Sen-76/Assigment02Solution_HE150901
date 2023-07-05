using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using DataAccess.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
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
                var result = await _orderService.GetList();
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
                var result = await _orderService.GetById(id);
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
        [HttpPost("Create")]
        public async Task<ApiResponse> Create(OrderVm newValue)
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
                var result = await _orderService.Create(newValue);
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
                var result = await _orderService.Delete(id);
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
        [HttpPost("Update")]
        public async Task<ApiResponse> Update(OrderVm newValue)
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
                var result = await _orderService.Update(newValue);
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
