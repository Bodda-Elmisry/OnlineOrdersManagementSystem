using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IUnitOfWork
{
    public interface IOrderUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IOrderStatusHistoryRepository OrderStatusHistoryRepository { get; }
        Task<IEnumerable<OrderItemsListResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto);
        Task<OrderItemsListResultDTO?> GetOrderByIdAsync(long orderId);
        Task<int> CompleteAsync();
    }
}
