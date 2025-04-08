using OnlineOrderManagementSystem.Domain.DTOs.Cust;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IRepository.Cust
{
    public interface ICustomerRepository
    {
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(long customerId);
        Task<Customer?> GetCustomerByIdAsync(long customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(GetCustomersDTO dto);
    }
}
