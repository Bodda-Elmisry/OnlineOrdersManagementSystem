﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Models.sal
{
    public class OrderItem
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
