using OnlineOrderManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public class AddOrderDTO
    {
        public long CustomerId { get; set; }
        public IEnumerable<AddOrderItemDTO> Items { get; set; }
    }
}
