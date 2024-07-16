using AutoMapper;
using OrderSystem.Core.Entities;
using OrderSystem.PL.DTOs;

namespace OrderSystem.PL.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order,OrderToReturnDto>();
            CreateMap<OrderItem, OrderItemToReturnDto>();
            CreateMap<Invoice, InvoiceToReturnDto>();
        }
    }
}
