﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public float UnitStock { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
