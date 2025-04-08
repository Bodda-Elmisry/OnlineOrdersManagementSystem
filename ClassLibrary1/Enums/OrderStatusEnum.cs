using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Canceled = 5,
    }

    public class OrderStatusEnumHelper
    {
        public static string GetDescription(OrderStatusEnum status)
        {
            return status switch
            {
                OrderStatusEnum.Pending => "Pending",
                OrderStatusEnum.Processing => "Processing",
                OrderStatusEnum.Shipped => "Shipped",
                OrderStatusEnum.Delivered => "Delivered",
                OrderStatusEnum.Canceled => "Canceled",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
