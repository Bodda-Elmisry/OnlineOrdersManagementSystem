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

        [HttpGet("customers/{Id}")]
        public async Task<IActionResult> GetCustomer(long Id)
        {
            var customers = await _customerService.GetCustomerByIdAsync(Id);
            return Ok(customers);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDTO dto)
        {
            var getCustomerDTO = new GetCustomersDTO(
                Name: null,
                Email: null,
                Address: null,
                PhoneNumber : dto.PhoneNumber, 
                PageNumber:1);

            var customer = await _customerService.GetAllCustomersAsync(getCustomerDTO);

            if(customer.Count() > 0)
            {
                return BadRequest("Customer with this phone number already exists");
            }

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
