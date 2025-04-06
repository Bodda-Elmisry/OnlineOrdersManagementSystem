using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Enums
{
    public enum ProductStatusEnum
    {
        orderd = 1,
        inPackaging = 2,
        inShipping = 3,
        delevered = 4,
        canceled = 5,
    }
}
