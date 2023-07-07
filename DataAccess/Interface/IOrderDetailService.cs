using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IOrderDetailService
    {
        Task<ApiResponse> Create(OrderDetailVM newValue);
        Task<ApiResponse> Update(OrderDetailVM newValue);
        Task<ApiResponse> Delete(Guid orderId, Guid productId);
        Task<ApiResponse> GetById(Guid orderId, Guid productId);
        Task<ApiResponse> GetList();
    }
}
