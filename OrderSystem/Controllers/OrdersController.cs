using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Service;
using OrderSystem.Core.Specofication;
using OrderSystem.PL.DTOs;
using OrderSystem.Repository.Data;
using System.Security.Claims;

namespace OrderSystem.PL.Controllers
{

    [Authorize]
    public class OrdersController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly OrderManagementDbContext _dbContext;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService, OrderManagementDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderService = orderService;
            _dbContext = dbContext;
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }


        [ProducesResponseType(typeof(OrderToReturnDto), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if(order == null)
                return NotFound(new { Message = "This Order Is Not Exist"});
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }


        [ProducesResponseType(typeof(OrderToReturnDto), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<ActionResult<OrderToReturnDto>> UpdateOrderStatus(int orderId, Status status)
        {

            var spec = new OrderSpecifications(orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if(order == null)
                return BadRequest(new {Message = "This Order Is Not Exist :(" });

            var result = await _orderService.UpdateStatusAsync(order,status);
            if (result == null)
                return BadRequest(new { Message = "An Error Happened When Update Status.. Try Again" });

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }


        [ProducesResponseType(typeof(OrderToReturnDto), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto order)
        {
            var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Customer = await _dbContext.Set<Customer>().Where(c=>c.Email == CustomerEmail).FirstOrDefaultAsync();
            var CustomerId = Customer.CustomerId;

            var Order = await _orderService.CreateOrderAsync(order.PaymentMethod, CustomerId, CustomerEmail!, order.OrderItems);

            if (Order is null)
                return BadRequest(new { Message = "There Is a Problem With Your Order" });

            var OrderMapped = _mapper.Map<Order, OrderToReturnDto>(Order);
            return Ok(OrderMapped);
        }
    }
}
