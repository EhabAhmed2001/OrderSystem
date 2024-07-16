using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.PL.DTOs;

namespace OrderSystem.PL.Controllers
{
    public class InvoiceController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(InvoiceToReturnDto), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceToReturnDto>>> GetInvoices()
        {
            var Invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            if(Invoices == null) 
                return NotFound("No Invoices Found");
            var MappedInvoices = _mapper.Map<IReadOnlyList<InvoiceToReturnDto>>(Invoices);

            return Ok(MappedInvoices);
        }


        [ProducesResponseType(typeof(InvoiceToReturnDto), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [Authorize(Roles = "Admin")]
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<InvoiceToReturnDto>> GetInvoiceById(int invoiceId)
        {
            var Invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);
            if (Invoice == null)
                return NotFound("No Invoice Found");
            var MappedInvoice = _mapper.Map<InvoiceToReturnDto>(Invoice);
            return Ok(MappedInvoice);
        }
    }
}
