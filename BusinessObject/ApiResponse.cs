using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ApiResponse
    {
        public ResponeStatus Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public enum ResponeStatus
    {
        Success = 200,
        ForBiden = 403,
        BadRequesr = 400,
        Authorie = 401,
        NotFound = 404,
        InternalServer = 500
    }
}
