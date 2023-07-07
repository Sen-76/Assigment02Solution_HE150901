using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class OrderDetailVM
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        [Required]
        public float Quantity { get; set; }
        [Required]
        public float Discount { get; set; }
    }
}
