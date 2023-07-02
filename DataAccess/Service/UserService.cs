using BusinessObject;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class UserService : CommonInterface<User>
    {
        public Task<ApiResponse> Create(User newValue)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Update(User newValue)
        {
            throw new NotImplementedException();
        }
    }
}
