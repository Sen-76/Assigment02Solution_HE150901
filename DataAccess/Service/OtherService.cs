using BusinessObject.Model;
using BusinessObject.ViewModel;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Service
{
    public class OtherService : IOtherService
    {
        private readonly EStoreAPContext _context;
        public OtherService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }

        public async Task<ApiResponse> Buy(BuyVM newValue)
        {
            try
            {
                var orderId = Guid.NewGuid();
                var order = new Order()
                {
                    MemberId = newValue.MemberId,
                    OrderId = orderId,
                    OrderDate = DateTime.UtcNow,
                    ShippedDate = DateTime.UtcNow,
                    RequiredDate = DateTime.UtcNow,
                    Freight = 1,
                };
                await _context.Orders.AddAsync(order);
                foreach (var buy in newValue.BuyProducts)
                {
                    var product = _context.Products.Where(x => x.ProductId == buy.ProductId).FirstOrDefault();
                    product.UnitStock = product.UnitStock - buy.Quantity;
                    _context.Products.Update(product);
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = orderId,
                        ProductId= product.ProductId,
                        UnitPrice= product.UnitPrice * buy.Quantity,
                        Quantity= buy.Quantity,
                        Discount= 0,
                    };
                    await _context.OrderDetails.AddAsync(orderDetail);
                }
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
            }catch(Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Message= ex.Message
                };
            }
        }
    }
}
