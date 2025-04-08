using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Sal;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using OnlineOrderManagementSystem.Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.UnitOfWork
{
    internal class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly IOptions<AppSettingDTO> appSettings;
        private readonly IMapper mapper;

        public OrderUnitOfWork(AppDbContext context,
                               IOptions<AppSettingDTO> appSettings,
                               IMapper mapper)
        {
            this.context = context;
            this.appSettings = appSettings;
            this.mapper = mapper;
        }

        public IOrderRepository OrderRepository => new OrderRepository(context, appSettings, mapper);

        public IOrderItemRepository OrderItemRepository => new OrderItemRepository(context, appSettings, mapper);

        public IOrderStatusHistoryRepository OrderStatusHistoryRepository => new OrderStatusHistoryRepository(context, mapper);


        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<IEnumerable<OrderItemsListResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto)
        {
            int pagesize = this.appSettings.Value.PageSize != null ? this.appSettings.Value.PageSize.Value : 30;

            var pagenumber = dto.pageNumber > 0 ? dto.pageNumber : 1;

            var query = context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

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
            var result = await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new OrderItemsListResultDTO
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.Name,
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
                }).FirstOrDefaultAsync(o=> o.Id == orderId);

            return result;
        }
    }
}
