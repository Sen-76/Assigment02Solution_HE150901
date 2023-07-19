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
    public class OrderService : IOrderService
    {
        private readonly EStoreAPContext _context;
        public OrderService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }

        public async Task<ApiResponse> Create(OrderVm newValue)
        {
            var order = new Order()
            {
                Freight = newValue.Freight,
                MemberId = newValue.MemberId,
                OrderDate = newValue.OrderDate,
                OrderId = Guid.NewGuid(),
                ShippedDate = newValue.ShippedDate,
                RequiredDate = newValue.RequiredDate,
            };
            await _context.Orders.AddAsync(order);
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

        public async Task<ApiResponse> Delete(Guid id)
        {
            var product = await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                _context.Orders.Remove(product);
                bool result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return new ApiResponse()
                    {
                        Success= false
                    };
                }
                return new ApiResponse()
                {
                    Success= true
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }

        public async Task<ApiResponse> GetById(Guid id)
        {
            var product = await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
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
        public async Task<ApiResponse> GetByUserId(string id)
        {
            var product = await _context.Orders.Where(x => x.MemberId == id).ToListAsync();
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
            var product = await _context.Orders.OrderBy(x => x.OrderDate).ToListAsync();
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

        public async Task<ApiResponse> Search(DateTime fromDate, DateTime endDate)
        {
            var result = _context.OrderDetails
               .Join(_context.Orders,
                   od => od.OrderId,
                   o => o.OrderId,
                   (od, o) => new { OrderDetail = od, Order = o })
                .Join(
                   _context.Products,
                   od => od.OrderDetail.ProductId,
                   p => p.ProductId,
                   (od, p) => new { od.OrderDetail, od.Order, Product = p }
               )
               .Where(x => x.Order.OrderDate >= fromDate && x.Order.OrderDate <= endDate)
               .GroupBy(
                   x => new
                   {
                       OrderDate = x.Order.OrderDate,
                       ProductName = x.Product.ProductName,
                       UnitPrice = x.OrderDetail.UnitPrice
                   })
               .Select(g => new
               {
                   OrderDate = g.Key.OrderDate,
                   ProductName = g.Key.ProductName,
                   UnitPrice = g.Key.UnitPrice,
                   Quantity = g.Sum(x => x.OrderDetail.Quantity),
                   Sales = g.Sum(x => x.OrderDetail.Quantity) * g.Key.UnitPrice
               })
               .OrderByDescending(x => x.OrderDate)
               .ThenByDescending(x => x.Sales)
               .ToList();
            if (result.Count > 0)
            {
                return new ApiResponse()
                {
                    Success = true,
                    Data = result
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }

        public async Task<ApiResponse> Update(OrderVm newValue)
        {
            var order = new Order()
            {
                Freight = newValue.Freight,
                MemberId = newValue.MemberId,
                OrderDate = newValue.OrderDate,
                OrderId = newValue.OrderId,
                ShippedDate = newValue.ShippedDate,
                RequiredDate = newValue.RequiredDate,
            };
            _context.Orders.Update(order);
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
