namespace OrderSystem.PL.DTOs
{
    public class InvoiceToReturnDto
    {
        public int InvoiceId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}
