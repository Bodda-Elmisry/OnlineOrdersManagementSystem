using MapsterMapper;
using OnlineOrderManagementSystem.Domain.DTOs.Cust;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Repository.IRepository.Cust;
using OnlineOrderManagementSystem.Repository.IServices.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Services.Cust
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddCustomerAsync(AddCustomerDTO customer)
        {
            var newCustomer = mapper.Map<Customer>(customer);
            return await customerRepository.AddCustomerAsync(newCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(long customerId)
        {
            return await customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(GetCustomersDTO dto)
        {
            return await customerRepository.GetAllCustomersAsync(dto);
        }

        public async Task<Customer?> GetCustomerByIdAsync(long customerId)
        {
            return await customerRepository.GetCustomerByIdAsync(customerId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            return await customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
