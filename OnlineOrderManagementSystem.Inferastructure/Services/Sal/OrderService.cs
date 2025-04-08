using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Inferastructure.Repositories;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Sal;
using OnlineOrderManagementSystem.Repository.IRepository;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
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
        private readonly AppSettingDTO appSettings;
        private readonly IMapper mapper;
        private readonly IBaseRepository<Product> productRepository;
        private readonly IBaseRepository<Order> orderRepository;
        private readonly IBaseRepository<OrderItem> orderItemRepository;
        private readonly IBaseRepository<OrderStatusHistory> orderStatusHistoryRepository;

        public OrderService(IOrderUnitOfWork orderUnitOfWork,
                               IOptions<AppSettingDTO> appSettings,
                               IMapper mapper)
        {
            this.orderUnitOfWork = orderUnitOfWork;
            this.appSettings = appSettings.Value;
            this.mapper = mapper;
            orderRepository = orderUnitOfWork.Repository<Order, BaseRepository<Order>>();
            orderItemRepository = orderUnitOfWork.Repository<OrderItem, BaseRepository<OrderItem>>();
            orderStatusHistoryRepository = orderUnitOfWork.Repository<OrderStatusHistory, BaseRepository<OrderStatusHistory>>();
            productRepository = orderUnitOfWork.Repository<Product, BaseRepository<Product>>();
        }

        public async Task<bool> CancelOrderAsync(long orderId)
        {
            
            var order = await orderRepository.GetByIdAsync(orderId);
            if(order == null)
            {
                return false;
            }

            order.Status = OrderStatusEnum.Canceled;

            orderRepository.Update(order);

            await CreateStatusHistory(order.Id, order.Status);

            return await orderUnitOfWork.CompleteAsync() > 0;
        }

        public async Task<string?> CreateOrderAsync(AddOrderDTO dto)
        {
            try
            {
                var errors = string.Empty;


                await orderUnitOfWork.BeginTransactionAsync();

                var newOrder = new Order
                {
                    CustomerId = dto.CustomerId,
                    OrderDate = DateTime.Now,
                    Status = OrderStatusEnum.Pending
                };

                newOrder = await orderRepository.AddAsync(newOrder);
                await orderUnitOfWork.CompleteAsync();



                foreach (var item in dto.Items)
                {
                    var product = await productRepository.GetByIdAsync(item.ProductId);
                    if (product.StockQuantity < item.Quantity)
                    {
                        if (errors.Length > 0)
                        {
                            errors += ", ";
                        }

                        errors += $"Product {product.Name} with quantity {product.StockQuantity} less than order request {item.Quantity} ";
                    }
                    else
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = newOrder.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Subtotal = item.Quantity * product.Price
                        };

                        
                        await orderItemRepository.AddAsync(orderItem);

                        product.StockQuantity -= item.Quantity;
                        
                        productRepository.Update(product);

                    }
                }

                if (!string.IsNullOrEmpty(errors))
                {
                    await orderUnitOfWork.RollbackTransactionAsync();
                    return errors;
                }  

                await CreateStatusHistory(newOrder.Id, newOrder.Status);

                await orderUnitOfWork.CommitTransactionAsync();

                return null;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<OrderItemsListResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto)
        {
            //return await orderUnitOfWork.GetAllOrdersAsync(dto);

            int pagesize = this.appSettings.PageSize != null ? this.appSettings.PageSize.Value : 30;

            var pagenumber = dto.pageNumber > 0 ? dto.pageNumber : 1;

            var query = orderRepository.GetAll();

            query.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);

            query = dto.customerId != null ? query.Where(o => o.CustomerId == dto.customerId) : query;
            query = dto.customerName != null ? query.Where(o => o.Customer.Name.Contains(dto.customerName)) : query;
            query = dto.orderDateMin != null ? query.Where(o => o.OrderDate >= dto.orderDateMin.Value.Date) : query;
            query = dto.orderDateMax != null ? query.Where(o => o.OrderDate <= dto.orderDateMax.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)) : query;
            query = dto.status != null ? query.Where(o => o.Status == dto.status) : query;

            var result = await query.Select(o => new OrderItemsListResultDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.Name,
                CustomerPhone = o.Customer.PhoneNumber,
                CustomerEmail = o.Customer.Email,
                CustomerAddress = o.Customer.Address,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                Items = o.OrderItems.Select(oi => new OrderItemResultDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    OrderId = oi.OrderId,
                    OrderDate = oi.Order.OrderDate,
                    Quantity = oi.Quantity,
                    Subtotal = oi.Subtotal
                }).ToList()
            }).OrderBy(o => o.OrderDate)
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();


            return result;
        }

        public async Task<OrderItemsListResultDTO?> GetOrderByIdAsync(long orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId, query =>
                                                                    query.Include(o => o.OrderItems)
                                                                     .ThenInclude(oi => oi.Product)
                                                                     .Include(o=> o.Customer));

            if (order != null)
            {
                return new OrderItemsListResultDTO
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer.Name,
                    CustomerPhone = order.Customer.PhoneNumber,
                    CustomerEmail = order.Customer.Email,
                    CustomerAddress = order.Customer.Address,
                    OrderDate = order.OrderDate,
                    Status = order.Status.ToString(),
                    Items = order.OrderItems.Select(oi => new OrderItemResultDTO
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        OrderId = oi.OrderId,
                        OrderDate = order.OrderDate,
                        Quantity = oi.Quantity,
                        Subtotal = oi.Subtotal
                    }).ToList()
                };
            }

            return null;
        }

        public async Task<bool> UpdateOrderStatusAsync(long orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }
            if (order.Status == OrderStatusEnum.Delivered)
            {
                return false;
            }

            order.Status++;

            orderRepository.Update(order);

            await CreateStatusHistory(order.Id, order.Status);

            return await orderUnitOfWork.CompleteAsync() > 0;
        }

        private async Task CreateStatusHistory(long orderId,OrderStatusEnum status)
        {
            var statusHistory = new OrderStatusHistory
            {
                OrderId = orderId,
                Status = status,
                CreateDate = DateTime.Now,
            };

            await orderStatusHistoryRepository.AddAsync(statusHistory);
        }




    }
}
