using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ProductService : IProductService
    {
        private readonly EStoreAPContext _context;
        public ProductService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }
        public async Task<ApiResponse> Create(ProductVM newValue)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                CategoryId = newValue.CategoryId,
                ProductName = newValue.ProductName,
                UnitPrice = newValue.UnitPrice,
                UnitStock = newValue.UnitStock,
            };
            await _context.Products.AddAsync(product);
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
                Success= true
            };
        }

        public async Task<ApiResponse> Delete(Guid id)
        {
            var product = await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                var orderDetail = await _context.OrderDetails.Where(x => x.ProductId == id).FirstOrDefaultAsync();
                if (orderDetail != null) _context.OrderDetails.Remove(orderDetail);
                _context.Products.Remove(product);
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
            var product = await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product != null)
            {
                return new ApiResponse()
                {
                    Success= true,
                    Data= product
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }

        public async Task<ApiResponse> GetList()
            {
            var product = await _context.Products.OrderBy(x => x.ProductName).ToListAsync();
            if (product.Count > 0)
            {
                return new ApiResponse()
                {
                    Success= true,
                    Data= product
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }

        public async Task<ApiResponse> Search(string serchValue)
        {
            var product = await _context.Products
                .Where(x => x.ProductName.Contains(serchValue))
                .OrderBy(x => x.ProductName).ToListAsync();
            if (product.Count > 0)
            {
                return new ApiResponse()
                {
                    Success= true,
                    Data= product
                };
            }
            return new ApiResponse()
            {
                Success = false
            };
        }

        public async Task<ApiResponse> Update(ProductVM newValue)
        {
            var product = new Product()
            {
                ProductId = newValue.ProductId,
                CategoryId = newValue.CategoryId,
                ProductName = newValue.ProductName,
                UnitPrice = newValue.UnitPrice,
                UnitStock = newValue.UnitStock,
            };
            _context.Products.Update(product);
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
                Success= true
            };
        }
    }
}
