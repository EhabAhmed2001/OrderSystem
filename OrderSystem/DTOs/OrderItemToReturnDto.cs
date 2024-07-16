namespace OrderSystem.PL.DTOs
{
    public class OrderItemToReturnDto
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
