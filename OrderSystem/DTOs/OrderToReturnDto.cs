using OrderSystem.Core.Entities;

namespace OrderSystem.PL.DTOs
{
    public class OrderToReturnDto
    {
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ICollection<OrderItemToReturnDto> OrderItems { get; set; } = new HashSet<OrderItemToReturnDto>();

    }
}
