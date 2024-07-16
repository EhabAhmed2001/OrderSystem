using OrderSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specofication
{
    public class CustomerSpecifications : Specifications<Order>
    {
        public CustomerSpecifications(int CustomerId) : base(o => o.CustomerId == CustomerId)
        {
            OrderBy = O => O.OrderDate;
        }
    }
}
