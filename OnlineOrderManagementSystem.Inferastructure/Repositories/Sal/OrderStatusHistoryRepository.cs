using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
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
    internal class OrderStatusHistoryRepository : IOrderStatusHistoryRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public OrderStatusHistoryRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddOrderStatusHistoryAsync(OrderStatusHistory orderStatusHistory)
        {
            context.OrderStatusHistory.Add(orderStatusHistory);
        }

        public async Task<IEnumerable<OrderStatusHistoryResultDTO>> GetAllOrderStatusHistoriesAsync(long orderId)
        {
            var query = await context.OrderStatusHistory
                                .Where(h => h.OrderId == orderId)
                                .OrderBy(h=> h.Id)
                                .ToListAsync();

            return mapper.Map<List<OrderStatusHistoryResultDTO>>(query);
        }
    }
}
