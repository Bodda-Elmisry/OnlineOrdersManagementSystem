using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IServices.Sal
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(AddOrderDTO dto);

        Task<bool> UpdateOrderStatusAsync(long orderId);

        Task<bool> CancelOrderAsync(long orderId);

        Task<IEnumerable<OrderItemsListResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto);

        Task<OrderItemsListResultDTO?> GetOrderByIdAsync(long orderId);
    }
}
