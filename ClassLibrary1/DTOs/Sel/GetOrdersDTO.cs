using OnlineOrderManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public record class GetOrdersDTO(
        long? customerId,
        string? customerName,
        DateTime? orderDateMin,
        DateTime? orderDateMax,
        OrderStatusEnum? status,
        int pageNumber);
}
