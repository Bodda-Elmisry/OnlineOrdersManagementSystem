using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.Enums
{
    public enum PropsLengthEnum
    {
        name = 200,
        email = 200,
        address = 500,
        phoneNumber = 15,
        description = 1000,
        pricePercision = 18,
        priceScale = 2,
    }
}
