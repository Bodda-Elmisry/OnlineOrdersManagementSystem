using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Sel
{
    public class OrderResultDTO(
        long Id,
        long CustomerId,
        string CustomerName,
        DateTime OrderDate,
        string Status
        );
}
