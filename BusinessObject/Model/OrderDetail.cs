﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        public float Discount { get; set; }
        public virtual Order Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}
