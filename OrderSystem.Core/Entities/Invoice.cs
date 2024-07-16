using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalAmount { get; set; }

        // Navigation property
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
