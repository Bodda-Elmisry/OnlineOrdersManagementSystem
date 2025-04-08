using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagementSystem.Domain.DTOs.Cust;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Repository.IRepository.Cust;
using OnlineOrderManagementSystem.Repository.IServices.Cust;

namespace OnlineOrderManagementSystem.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] GetCustomersDTO dto)
        {
            var customers = await _customerService.GetAllCustomersAsync(dto);
            return Ok(customers);
        }

        [HttpGet("customer/{Id}")]
        public async Task<IActionResult> GetCustomer(long Id)
        {
            var customers = await _customerService.GetCustomerByIdAsync(Id);
            return Ok(customers);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> AddCustomer(AddCustomerDTO dto)
        {
            var added = await _customerService.AddCustomerAsync(dto);
            return Ok(added);
        }

        [HttpPut("customers")]
        public async Task<IActionResult> UpdateCustomer(Customer dto)
        {
            var updated = await _customerService.UpdateCustomerAsync(dto);
            return Ok(updated);
        }


    }
}
