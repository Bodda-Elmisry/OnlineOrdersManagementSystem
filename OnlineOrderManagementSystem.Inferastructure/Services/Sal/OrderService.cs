using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Repository.IServices.Sal;
using OnlineOrderManagementSystem.Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Services.Sal
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderUnitOfWork orderUnitOfWork;

        public OrderService(IOrderUnitOfWork orderUnitOfWork)
        {
            this.orderUnitOfWork = orderUnitOfWork;
        }

        public async Task<bool> CancelOrderAsync(long orderId)
        {
            var order = await orderUnitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
            if(order == null)
            {
                return false;
            }

            order.Status = OrderStatusEnum.Canceled;

            orderUnitOfWork.OrderRepository.UpdateOrderAsync(order);
            return await orderUnitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> CreateOrderAsync(AddOrderDTO dto)
        {
            var newOrder = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.Now,
                Status = OrderStatusEnum.Pending
            };

            orderUnitOfWork.OrderRepository.CreateOrderAsync(newOrder);

            foreach (var item in dto.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal
                };
                orderUnitOfWork.OrderItemRepository.AddOrderItemAsync(orderItem);
            }

            var orderStatusHistory = new OrderStatusHistory
            {
                OrderId = newOrder.Id,
                Status = OrderStatusEnum.Pending,
                CreateDate = DateTime.Now
            };

            orderUnitOfWork.OrderStatusHistoryRepository.AddOrderStatusHistoryAsync(orderStatusHistory);

            return await orderUnitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<OrderItemsListResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto)
        {
            return await orderUnitOfWork.GetAllOrdersAsync(dto);
        }

        public async Task<OrderItemsListResultDTO?> GetOrderByIdAsync(long orderId)
        {
            return await orderUnitOfWork.GetOrderByIdAsync(orderId);
        }

        public async Task<bool> UpdateOrderStatusAsync(long orderId)
        {
            var order = await orderUnitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
            if(order == null)
            {
                return false;
            }
            if(order.Status == OrderStatusEnum.Delivered)
            {
                return false;
            }

            order.Status++;

            orderUnitOfWork.OrderRepository.UpdateOrderAsync(order);

            return await orderUnitOfWork.CompleteAsync() > 0;


        }
    }
}
