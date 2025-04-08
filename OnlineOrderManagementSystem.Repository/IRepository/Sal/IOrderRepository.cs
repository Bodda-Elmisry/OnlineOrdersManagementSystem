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
        Task<IEnumerable<OrderResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto);
        Task<OrderResultDTO?> GetOrderViewByIdAsync(long orderId);
        Task<Order?> GetOrderByIdAsync(long orderId);
        void CreateOrderAsync(Order order);
        void UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(long orderId);
    }
}
