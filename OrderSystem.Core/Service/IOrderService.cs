using OrderSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Service
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(PaymentMethods paymentMethods, int CustomerId, string Customeremail, ICollection<CreateOrderItem> orderItems);
        public Task<Order?> UpdateStatusAsync(Order order, Status status);
    }
}
