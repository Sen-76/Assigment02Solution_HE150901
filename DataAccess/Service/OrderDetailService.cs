using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly EStoreAPContext _context;
        public OrderDetailService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }
        public async Task<ApiResponse> Create(OrderDetailVM newValue)
        {
            var orderDetail = new OrderDetail()
            {
                OrderId = newValue.OrderId,
                ProductId= newValue.ProductId,
                UnitPrice= newValue.UnitPrice,
                Quantity= newValue.Quantity,
                Discount= newValue.Discount,
            };
            await _context.OrderDetails.AddAsync(orderDetail);
            bool result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return new ApiResponse()
                {
                    Success = false
                };
            }
            return new ApiResponse()
            {
                Success = true
            };
        }
        public async Task<ApiResponse> Delete(Guid orderId, Guid productId)
        {
            var orderDetail = await _context.OrderDetails.Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefaultAsync();
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                bool result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return new ApiResponse()
                    {
                        Success = false
                    };
                }
                return new ApiResponse()
                {
                    Success = true
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }
        public async Task<ApiResponse> GetById(Guid orderId, Guid productId)
        {
            var product = await _context.OrderDetails.Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                return new ApiResponse()
                {
                    Success = true,
                    Data = product
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }
        public async Task<ApiResponse> GetList()
        {
            var product = await _context.OrderDetails.OrderBy(x => x.OrderId).ToListAsync();
            if (product.Count > 0)
            {
                return new ApiResponse()
                {
                    Success = true,
                    Data = product
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }
        public async Task<ApiResponse> Update(OrderDetailVM newValue)
        {
            var orderDetail = new OrderDetail()
            {
                OrderId = newValue.OrderId,
                ProductId = newValue.ProductId,
                UnitPrice = newValue.UnitPrice,
                Quantity = newValue.Quantity,
                Discount = newValue.Discount,
            };
            _context.OrderDetails.Update(orderDetail);
            bool result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return new ApiResponse()
                {
                    Success = false
                };
            }
            return new ApiResponse()
            {
                Success = true
            };
        }
    }
}
