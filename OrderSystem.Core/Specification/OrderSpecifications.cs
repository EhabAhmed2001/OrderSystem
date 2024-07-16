using OrderSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Specofication
{
    public class OrderSpecifications : Specifications<Order>
    {
        public OrderSpecifications(int OrderId):base(O=>O.OrderId == OrderId)
        {
            Includes.Add(O => O.Customer);
        }
    }
}
