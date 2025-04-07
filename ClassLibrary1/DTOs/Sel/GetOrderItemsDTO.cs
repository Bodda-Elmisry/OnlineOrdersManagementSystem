using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public record class GetOrderItemsDTO(
        long orderId,
        long? productId,
        string? productName,
        int? quantity,
        decimal? subtotal,
        int pageNumber
        );
}
