using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public class AddOrderItemDTO
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        //public decimal Subtotal { get; set; }
    }
}
