using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IRepository.Sal
{
    public interface IOrderItemRepository
    {
        void AddOrderItemAsync(OrderItem item);
        void UpdateOrderItemAsync(OrderItem item);
        Task DeleteOrderItemAsync(long orderId, long productId);
        Task<IEnumerable<OrderItemResultDTO>> GetOrderItemsAsync(GetOrderItemsDTO dto);
    }
}
