using Microsoft.Extensions.DependencyInjection;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Cust;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Sal;
using OnlineOrderManagementSystem.Inferastructure.Services.Cust;
using OnlineOrderManagementSystem.Inferastructure.Services.Sal;
using OnlineOrderManagementSystem.Inferastructure.UnitOfWork;
using OnlineOrderManagementSystem.Repository.IRepository.Cust;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using OnlineOrderManagementSystem.Repository.IServices.Cust;
using OnlineOrderManagementSystem.Repository.IServices.Sal;
using OnlineOrderManagementSystem.Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Configrations
{
    public static class DependencyInjectionConfigration
    {
        #region Repositories
        public static void ConfigerRepos(IServiceCollection services)
        {
            ConfigerCustRepos(services);
            ConfigerSalRepos(services);
        }

        private static void ConfigerCustRepos(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        private static void ConfigerSalRepos(IServiceCollection services)
        {
            services.AddScoped<IProductRepoistory, ProductRepoistory>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>();

        }

        #endregion

        #region UnitOfWorks
        public static void ConfigerUOWs(IServiceCollection services)
        {
            ConfigerCustUOWs(services);
            ConfigerSalUOWs(services);
        }

        private static void ConfigerCustUOWs(IServiceCollection services)
        {
        }

        private static void ConfigerSalUOWs(IServiceCollection services)
        {
            services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

        }

        #endregion

        #region Services
        public static void ConfigerServices(IServiceCollection services)
        {
            ConfigerCustServices(services);
            ConfigerSalServices(services);
        }

        private static void ConfigerCustServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private static void ConfigerSalServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

        }

        #endregion
    }
}
