﻿using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IProductService : CommonInterface<ProductVM>
    {
        public Task<ApiResponse> Search(string searchValue);
    }
}
