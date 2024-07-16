using OrderSystem.Core.Entities;

namespace OrderSystem.PL.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
