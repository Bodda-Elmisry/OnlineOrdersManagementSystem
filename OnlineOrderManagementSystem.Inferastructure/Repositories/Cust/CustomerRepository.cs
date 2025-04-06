using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Domain.DTOs.Cust;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Repository.IRepository.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Repositories.Cust
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            context.Customers.Add(customer);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return false;
            }
            context.Customers.Remove(customer);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(GetCustomersDTO dto)
        {
            var query = context.Customers.AsQueryable();

            query = dto.Name != null ? query.Where(c => c.Name.Contains(dto.Name)) : query;
            query = dto.Email != null ? query.Where(c => c.Email.Contains(dto.Email)) : query;
            query = dto.Address != null ? query.Where(c => c.Address.Contains(dto.Address)) : query;
            query = dto.PhoneNumber != null ? query.Where(c => c.PhoneNumber.Contains(dto.PhoneNumber)) : query;

            return await query.OrderBy(c=> c.Name).ToListAsync();

        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            context.Customers.Update(customer);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
