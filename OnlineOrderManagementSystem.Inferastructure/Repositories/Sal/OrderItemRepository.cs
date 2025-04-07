using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Enums;
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
    internal class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext context;
        private readonly AppSettingDTO appSettings;

        public OrderItemRepository(AppDbContext context,
                                   IOptions<AppSettingDTO> appSettings,
                                   IMapper mapper)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public void AddOrderItemAsync(OrderItem item)
        {
            context.OrderItems.Add(item);
        }

        public async Task DeleteOrderItemAsync(long orderId, long productId)
        {
            var item = await context.OrderItems
                .FirstOrDefaultAsync(i => i.OrderId == orderId && i.ProductId == productId);

            if (item != null)
            {
                context.Remove(item);
            }
        }

        public async Task<IEnumerable<OrderItemResultDTO>> GetOrderItemsAsync(GetOrderItemsDTO dto)
        {
            int pagesize = this.appSettings.PageSize != null ? this.appSettings.PageSize.Value : 30;

            var pagenumber = dto.pageNumber > 0 ? dto.pageNumber : 1;

            var query = context.OrderItems
                .Include(i => i.Product)
                .Include(i => i.Order)
                .Where(i => i.OrderId == dto.orderId);

            query = dto.productId != null ? query.Where(i => i.ProductId == dto.productId) : query;
            query = dto.productName != null ? query.Where(i => i.Product.Name.Contains(dto.productName)) : query;
            query = dto.quantity != null ? query.Where(i => i.Quantity == dto.quantity) : query;
            query = dto.subtotal != null ? query.Where(i => i.Subtotal == dto.subtotal) : query;

            //return await query.Select(o => new OrderItemResultDTO(
            //                                    o.OrderId,
            //                                    o.Order.OrderDate,
            //                                    o.ProductId,
            //                                    o.Product.Name,
            //                                    o.Quantity,
            //                                    o.Subtotal
            //                                    ))
            //                .Skip((pagenumber - 1) * pagesize)
            //                .Take(pagesize)
            //                .ToListAsync();

            return await query.ProjectToType<OrderItemResultDTO>()
                .OrderBy(i => i.ProductName)
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();


        }

        public void UpdateOrderItemAsync(OrderItem item)
        {
            context.OrderItems.Update(item);
        }
    }
}
