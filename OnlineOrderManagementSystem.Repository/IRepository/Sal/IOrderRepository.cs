using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IRepository.Sal
{
    public interface IOrderRepository
    {
        // Define methods for order management
        Task<IEnumerable<Order>> GetAllOrdersAsync(GetOrdersDTO dto);
        Task<Order?> GetOrderByIdAsync(int orderId);
        void CreateOrderAsync(Order order);
        void UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
