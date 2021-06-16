using System;
using System.Collections.Generic;
using System.Text;

namespace Mmt.Tech.Data
{
    public class CustomerOrder
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerId { get; set; }

        public DateTime deliveryExpected { get; set; }

        public IList<CustomerOrderItem> OrderItems { get; set; }
     }

    public class CustomerOrderItem
    {
        public string Product { get; set; }

        public int Quantity { get; set; }

        public decimal PriceEach { get; set; }
    }
}
