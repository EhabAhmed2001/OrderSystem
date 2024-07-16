using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace OrderSystem.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Order?> CreateOrderAsync(PaymentMethods paymentMethods,int CustomerId, string Customeremail, ICollection<CreateOrderItem> orderItems)
        {
            var OrderItems = new List<OrderItem>();
            decimal Total = 0M;

            foreach (var item in orderItems)
            {
                decimal Discount = 0;
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                var ProductItem = new Product(product.Name, product.Price, product.Stock);
                if (item.Quantity * product.Price >= 100)
                    Discount = 0.05M;
                else if (item.Quantity * product.Price >= 200)
                    Discount = 0.1M;
                var OrderItem = new OrderItem(item.Quantity, product.Price, Discount, product.ProductId);
                Total += item.Quantity * product.Price;

                OrderItems.Add(OrderItem);
            }

            if (Total >= 100)
                Total -= Total * 0.05M;
            else if (Total >= 200)
                Total -= Total * 0.1M;

            var order = new Order(Total, paymentMethods, OrderItems, CustomerId);

            await _unitOfWork.Repository<Order>().AddAsync(order);

            if (await _unitOfWork.CompleteAsync() > 0)
            {
                var email = new Email()
                {
                    To = Customeremail,
                    Subject = "Change Order Status",
                    Body = "Your Order Now Is Pending"
                };
                SendEmailAsync(email);
                return order;
            }
            return null;
        }


        public async Task<Order?> UpdateStatusAsync(Order order ,Status status)
        {
            order.Status = status;
            if(await _unitOfWork.CompleteAsync() > 0)
            {
                var email = new Email()
                {
                    To = order.Customer.Email,
                    Subject = "Change Order Status",
                    Body = $"Your Order Now Is {status}"
                };
                SendEmailAsync(email);
                return order;
            }
            return null;            

        }


        private static void SendEmailAsync(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ehab.ahmedhemida@gmail.com", "zhhkpbcaxlmzegsj");
            client.Send("ehab.ahmedhemida@gmail.com", email.To, email.Subject, email.Body);
        }

    }
}
