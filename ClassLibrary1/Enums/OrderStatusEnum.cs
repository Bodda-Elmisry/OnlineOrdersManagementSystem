using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Enums
{
    public enum OrderStatusEnum
    {
        orderd = 1,
        inPackaging = 2,
        inShipping = 3,
        delevered = 4,
        canceled = 5,
    }

    public class OrderStatusEnumHelper
    {
        public static string GetDescription(OrderStatusEnum status)
        {
            return status switch
            {
                OrderStatusEnum.orderd => "Ordered",
                OrderStatusEnum.inPackaging => "In Packaging",
                OrderStatusEnum.inShipping => "In Shipping",
                OrderStatusEnum.delevered => "Delivered",
                OrderStatusEnum.canceled => "Canceled",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
