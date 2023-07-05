using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class ProductVM
    {
        [Required]
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        [Required]
        public float UnitStock { get; set; }
    }
}
