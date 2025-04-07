using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
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

    }
}
