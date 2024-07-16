using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Specofication;
using OrderSystem.PL.DTOs;
using OrderSystem.Repository.Data;

namespace OrderSystem.PL.Controllers
{
    public class CustomersController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, OrderManagementDbContext dbContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(Ok), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerDto CustomerData)
        {
            var IsEmailExist = await _dbContext.Set<Customer>().Where(C=>C.Email.Equals(CustomerData.CustomerEmail)).AnyAsync();
            if (IsEmailExist)
                return BadRequest(new { Message = "This Email Is Already Exist" });
            var CustomerMapped = new Customer() { Email = CustomerData.CustomerEmail, Name = CustomerData.CustomerName };
            await _unitOfWork.Repository<Customer>().AddAsync(CustomerMapped);
            if(await _unitOfWork.CompleteAsync() > 0) 
                return Ok(new { Message = "Created Successfully" });
            return BadRequest(new { Message = "An Error Happened When Create New Customer.. Try Again" });
        }

        [Authorize]
        [HttpGet("{CustomerId}/orders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForCustomer(int CustomerId)
        {
            var Spec = new CustomerSpecifications(CustomerId);

            var orderss = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            if (orderss == null)
                return NotFound(new { Message = "No Orders Found" });
            var MappedOrders = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orderss);
            return Ok(MappedOrders);
        }
    }
}
