using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public record class GetProductsDTO(
        string? name,
        string? description,
        decimal? minPrice,
        decimal? maxPrice,
        int? minQuantity,
        int? maxQuantity,
        int pageNumber
        );
}
