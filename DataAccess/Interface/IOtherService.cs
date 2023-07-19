using BusinessObject;
using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IOtherService
    {
        Task<ApiResponse> Buy(BuyVM newValue);
    }
}
