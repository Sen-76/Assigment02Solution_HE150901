using BusinessObject;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly IOtherService _otherService;
        public OtherController(IOtherService otherService)
        {
            _otherService = otherService;
        }
        [HttpPost("Buy")]
        [Authorize(Roles = "User")]
        public async Task<ApiResponse> Buy(BuyVM newValue)
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
                var result = await _otherService.Buy(newValue);
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
