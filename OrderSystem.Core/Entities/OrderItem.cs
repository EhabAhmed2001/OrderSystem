using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }

        public OrderItem(int quantity, decimal unitPrice, decimal discount, int productId)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            ProductId = productId;
        }

        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        // Navigation properties
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
