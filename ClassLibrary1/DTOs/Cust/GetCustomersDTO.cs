using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.DTOs.Cust
{
    public record class GetCustomersDTO(
        string Name,
        string Email,
        string Address,
        string PhoneNumber,
        int PageNumber
        );
}
