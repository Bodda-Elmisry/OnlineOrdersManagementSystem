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
        public int Status { get; set; }
    }
}
