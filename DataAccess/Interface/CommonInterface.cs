using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface CommonInterface<T>
    {
        public Task<ApiResponse> GetList();
        public Task<ApiResponse> Create(T newValue);
        public Task<ApiResponse> Update(T newValue);
        public Task<ApiResponse> Delete(Guid id);
        public Task<ApiResponse> GetById(Guid id);
    }
}
