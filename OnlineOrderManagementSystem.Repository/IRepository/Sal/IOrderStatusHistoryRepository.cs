using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IRepository.Sal
{
    public interface IOrderStatusHistoryRepository
    {
        void AddOrderStatusHistoryAsync(OrderStatusHistory orderStatusHistory);
        Task<IEnumerable<OrderStatusHistoryResultDTO>> GetAllOrderStatusHistoriesAsync(long orderId);
    }
}
