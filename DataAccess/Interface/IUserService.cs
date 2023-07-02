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
    public interface IUserService : CommonInterface<User>
    {
        Task<ApiResponse> Regis(UserVM newValue);
        Task<ApiResponse> Login(UserVM newValue);
        Task<ApiResponse> GetUserById(string id);

    }
}
