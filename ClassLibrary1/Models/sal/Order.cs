using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Models.sal
{
    public class Order : BaseModel
    {
        public long CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatusEnum Status { get; set; }

        public Customer Customer { get; set; }
    }
}
