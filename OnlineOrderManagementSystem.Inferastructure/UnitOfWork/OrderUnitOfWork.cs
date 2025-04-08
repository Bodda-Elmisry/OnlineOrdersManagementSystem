using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Inferastructure.Repositories;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Sal;
using OnlineOrderManagementSystem.Repository.IRepository;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using OnlineOrderManagementSystem.Repository.IUnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.UnitOfWork
{
    internal class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction? _dbContextTransaction;
        //private readonly IOptions<AppSettingDTO> appSettings;
        //private readonly IMapper mapper;
        //private IOrderRepository orderRepository;
        //private IOrderItemRepository orderItemRepository;
        //private IOrderStatusHistoryRepository orderStatusHistoryRepository;
        //private IProductRepoistory productRepoistory;
        private Hashtable? _repositories;

        //public OrderUnitOfWork(AppDbContext context,
        //                       IOptions<AppSettingDTO> appSettings,
        //                       IMapper mapper)
        public OrderUnitOfWork(AppDbContext context)
        {
            this.context = context;
            //this.appSettings = appSettings;
            //this.mapper = mapper;
        }

        public TRepository Repository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IBaseRepository<TEntity>
        {
            _repositories ??= new Hashtable();

            string type = $"{typeof(TEntity).Name}_{typeof(TRepository).Name}";

            if (!_repositories.ContainsKey(type))
            {
                object? repositoryInstance;

                Type repositoryType = typeof(TRepository);

                repositoryInstance = repositoryType.IsGenericTypeDefinition
                    ? Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context)
                    : Activator.CreateInstance(repositoryType, context);
                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type]!;
        }

        //public IOrderRepository OrderRepository => orderRepository ??= new OrderRepository(context, appSettings, mapper);

        //public IOrderItemRepository OrderItemRepository => orderItemRepository ??= new OrderItemRepository(context, appSettings, mapper);

        //public IOrderStatusHistoryRepository OrderStatusHistoryRepository => orderStatusHistoryRepository ??= new OrderStatusHistoryRepository(context, mapper);

        //public IProductRepoistory ProductRepoistory => productRepoistory ??= new ProductRepoistory(context, appSettings);

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _dbContextTransaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_dbContextTransaction == null)
            {
                throw new InvalidOperationException("No transaction started.");
            }

            try
            {
                _ = await CompleteAsync();
                await _dbContextTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await RollbackTransactionAsync();
                throw new InvalidOperationException("Transaction commit failed.", ex);
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_dbContextTransaction == null)
            {
                throw new InvalidOperationException("No transaction started.");
            }

            try
            {
                await _dbContextTransaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Transaction rollback failed.", ex);
            }
            finally
            {
                _dbContextTransaction.Dispose();
                _dbContextTransaction = null;
            }
        }
        

        
    }
}
