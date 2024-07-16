using OrderSystem.Core.Entities;
using OrderSystem.Core.Service;

namespace OrderSystem.PL.DTOs
{
    public class OrderDto
    {
        public PaymentMethods PaymentMethod { get; set; }
        public ICollection<CreateOrderItem> OrderItems { get; set; } = new HashSet<CreateOrderItem>();

    }
}
