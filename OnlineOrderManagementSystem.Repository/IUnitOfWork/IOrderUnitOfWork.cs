using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Repository.IRepository;
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
        //IOrderRepository OrderRepository { get; }
        //IOrderItemRepository OrderItemRepository { get; }
        //IOrderStatusHistoryRepository OrderStatusHistoryRepository { get; }
        //IProductRepoistory ProductRepoistory { get; }
        TRepository Repository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IBaseRepository<TEntity>;

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task<int> CompleteAsync();
    }
}
