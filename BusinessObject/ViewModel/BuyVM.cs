using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class BuyVM
    {
        public List<BuyProductVM>? BuyProducts { get; set; }
        public string MemberId { get; set; }
    }
    public class BuyProductVM
    {
        public Guid ProductId { get; set; }
        public float Quantity { get; set; }
    }
}
