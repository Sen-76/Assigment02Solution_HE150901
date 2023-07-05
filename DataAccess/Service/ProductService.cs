using BusinessObject;
using BusinessObject.Model;
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
    internal class ProductService : CommonInterface<Product>
    {
        private readonly EStoreAPContext _context;
        public ProductService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }
        public async Task<ApiResponse> Create(Product newValue)
        {
            await _context.Products.AddAsync(newValue);
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
            var orderDetail = await _context.OrderDetails.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product != null && orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.Products.Remove(product);
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
            var product = await _context.Products.Where(x => x.ProductId == id).Include(x => x.Category).FirstOrDefaultAsync();
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
            var product = await _context.Products.Include(x => x.Category).OrderBy(x => x.ProductName).ToListAsync();
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

        public async Task<ApiResponse> Update(Product newValue)
        {
            _context.Products.Update(newValue);
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
