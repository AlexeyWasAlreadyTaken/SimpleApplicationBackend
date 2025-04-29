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

            var Order = _mapper.Map<OrderDTO>(_repository.GetOrder(OrderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Order);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromBody] OrderDTO OrderDto)
        {
            if (OrderDto == null)
                return BadRequest("Order data is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Order = _mapper.Map<Order>(OrderDto);
            _repository.CreateOrder(Order);

            return Ok(Order);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(Guid id, [FromBody] OrderDTO OrderDto)
        {
            if (OrderDto == null)
                return BadRequest("Order data is null");

            if (id != OrderDto.Id)
                return BadRequest("different ids");

            if (!_repository.OrderExists(id))
                return BadRequest("Specified Order not exists in db");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Order = _mapper.Map<Order>(OrderDto);

            if (!_repository.UpdateOrder(Order))
            {
                ModelState.AddModelError("", "Somthing went wrong updating Order");
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
            if (!_repository.OrderExists(OrderId))
                return NotFound();

            var OrderToDelete = _repository.GetOrder(OrderId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.DeleteOrder(OrderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Order");
            }

            return NoContent();
        }

    }
}
