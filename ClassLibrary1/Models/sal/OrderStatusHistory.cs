using OnlineOrderManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Models.sal
{
    public class OrderStatusHistory : BaseModel
    {
        public long OrderId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime CreateDate { get; set; }

        public Order Order { get; set; }
    }
}
