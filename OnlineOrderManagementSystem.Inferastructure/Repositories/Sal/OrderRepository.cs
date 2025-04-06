using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Repositories.Sal
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly AppSettingDTO appSettings;

        public OrderRepository(AppDbContext context,
                                                IOptions<AppSettingDTO> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public void CreateOrderAsync(Order order)
        {
            context.Orders.Add(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if(order == null)
            {
                return;
            }
            context.Orders.Remove(order);
        }

        public async Task<IEnumerable<OrderResultDTO>> GetAllOrdersAsync(GetOrdersDTO dto)
        {
            int pagesize = this.appSettings.PageSize != null ? this.appSettings.PageSize.Value : 30;

            var pagenumber = dto.pageNumber > 0 ? dto.pageNumber : 1;

            var query = context.Orders.Include(o=> o.Customer).AsQueryable();

            query = dto.customerId != null ? query.Where(o => o.CustomerId == dto.customerId) : query;
            query = dto.customerName != null ? query.Where(o => o.Customer.Name.Contains(dto.customerName)) : query;
            query = dto.orderDateMin != null ? query.Where(o => o.OrderDate >= dto.orderDateMin.Value.Date) : query;
            query = dto.orderDateMax != null ? query.Where(o => o.OrderDate <= dto.orderDateMax.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)) : query;
            query = dto.status != null ? query.Where(o => o.Status == dto.status) : query;

            return await query.Select(o=> new OrderResultDTO(
                                                o.Id,
                                                o.CustomerId,
                                                o.Customer.Name,
                                                o.OrderDate,
                                                OrderStatusEnumHelper.GetDescription(o.Status)
                                                ))
                            .Skip((pagenumber - 1) * pagesize)
                            .Take(pagesize)
                            .ToListAsync();


        }

        public async Task<OrderResultDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o=> o.Id == orderId);
            if(order == null)
            {
                return null;
            }
            return new OrderResultDTO(
                order.Id,
                order.CustomerId,
                order.Customer.Name,
                order.OrderDate,
                OrderStatusEnumHelper.GetDescription(order.Status)
                );
        }

        public void UpdateOrderAsync(Order order)
        {
            context.Orders.Update(order);
        }
    }
}
