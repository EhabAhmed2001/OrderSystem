using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Entities
{
    public class Order
    {
        public Order()
        {
            
        }
        public Order(decimal totalAmount, PaymentMethods paymentMethod, ICollection<OrderItem> orderItems, int customerId)
        {
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            OrderItems = orderItems;
            CustomerId = customerId;
        }
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public PaymentMethods PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        // Navigation property
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
