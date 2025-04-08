using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Repository.IServices.Sal;

namespace OnlineOrderManagementSystem.API.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersDTO dto)
        {
            var orders = await _orderService.GetAllOrdersAsync(dto);
            return Ok(orders);
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrderById(long orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderDTO dto)
        {
            var result = await _orderService.CreateOrderAsync(dto);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok();
        }

        [HttpPut("orders/{orderId}")]
        public async Task<IActionResult> UpdateOrder(long orderId)
        {
            var result = await _orderService.UpdateOrderStatusAsync(orderId);
            if (!result)
            {
                return BadRequest("Error in change status");
            }
            return Ok();
        }

        [HttpPut("orders/{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(long orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);
            if (!result)
            {
                return BadRequest("Error in cancel order");
            }
            return Ok();
        }


    }
}
