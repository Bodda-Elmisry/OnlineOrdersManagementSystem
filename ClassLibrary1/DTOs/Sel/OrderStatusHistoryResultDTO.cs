﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public class OrderStatusHistoryResultDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
