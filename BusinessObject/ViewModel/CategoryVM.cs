using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class CategoryVM
    {
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
