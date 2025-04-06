using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Domain.ModelsConfigrations.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyCustConfigrateions(modelBuilder);
        }

        private void ApplyCustConfigrateions(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfigration());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
