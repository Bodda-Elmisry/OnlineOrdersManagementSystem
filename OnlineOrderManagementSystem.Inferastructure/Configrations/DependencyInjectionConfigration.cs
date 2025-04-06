﻿using Microsoft.Extensions.DependencyInjection;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Cust;
using OnlineOrderManagementSystem.Inferastructure.Repositories.Sal;
using OnlineOrderManagementSystem.Repository.IRepository.Cust;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
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
        }

        private static void ConfigerSalServices(IServiceCollection services)
        {

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

        }

        #endregion
    }
}
