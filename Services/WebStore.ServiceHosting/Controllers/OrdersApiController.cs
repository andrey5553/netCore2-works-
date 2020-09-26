using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrdersService
    {
        private readonly IOrdersService _orderService;

        public OrdersApiController(IOrdersService orderService) => _orderService = orderService;

        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName)
        {
            return await _orderService.GetUserOrders(UserName);
        }

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await _orderService.GetOrderById(id);
        }

        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel)
        {
            return await _orderService.CreateOrder(UserName, OrderModel);
        }
    }
}