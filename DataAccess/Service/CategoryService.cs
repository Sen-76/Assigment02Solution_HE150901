using BusinessObject.Model;
using BusinessObject.ViewModel;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Service
{
    public class CategoryService : ICateService
    {
        private readonly EStoreAPContext _context;
        public CategoryService(EStoreAPContext eStoreAp)
        {
            _context = eStoreAp;
        }

        public async Task<ApiResponse> Create(CategoryVM newValue)
        {
            var cate = new Category()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = newValue.CategoryName,
            };
            await _context.Categories.AddAsync(cate);
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
            var product = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
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

        public async Task<ApiResponse> GetById(Guid id)
        {
            var product = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
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
            var product = await _context.Categories.OrderBy(x => x.CategoryName).ToListAsync();
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

        public async Task<ApiResponse> Update(CategoryVM newValue)
        {
            var cate = new Category()
            {
                CategoryId = newValue.CategoryId,
                CategoryName = newValue.CategoryName,
            };
            _context.Categories.Update(cate);
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
