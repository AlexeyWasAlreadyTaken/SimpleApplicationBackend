using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleApplicationBack.DTO;
using SimpleApplicationBack.Interfaces;
using SimpleApplicationBack.Models;
using SimpleApplicationBack.Repository;

namespace SimpleApplicationBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _repository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _repository.GetOrders();

            var OrdersDto = _mapper.Map<List<OrderDTO>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(OrdersDto);
        }

        [HttpGet("{OrderId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(Guid OrderId)
        {
            if (!_repository.OrderExists(OrderId))
                return NotFound();

            var orders = _repository.GetOrder(OrderId);

            var OrdersDto = _mapper.Map<OrderDTO>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(OrdersDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            if (orderDto == null)
                return BadRequest("Order data is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);

            foreach (var op in order.OrderProducts)
            {
                op.Product = null;
            }

            _repository.CreateOrder(order);

            var orderToReturn = _mapper.Map<OrderDTO>(order);
            return Ok(orderToReturn);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(Guid id, [FromBody] OrderCreateDTO orderDto)
        {
            if (orderDto == null)
                return BadRequest("Order data is null");

            if (id != orderDto.Id)
                return BadRequest("Different IDs");

            if (!_repository.OrderExists(id))
                return NotFound("Specified order does not exist in DB");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);

            foreach (var op in order.OrderProducts)
            {
                op.Product = null;
            }

            if (!_repository.UpdateOrder(order))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{OrderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(Guid OrderId)
        {
            //TODO 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.OrderExists(OrderId))
                return NotFound();

            var orderToDelete = _repository.GetOrderWithOrderProducts(OrderId); 

            foreach (var op in orderToDelete.OrderProducts.ToList())
            {
                _repository.DeleteOrderProduct(op);
            }

            if (!_repository.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
