using System;
using System.Collections.Generic;
using System.Text;

namespace Mmt.Tech.Services
{

    public class RecentCustomerOrder
    {
        public Customer customer { get; set; }
        public Order order { get; set; }

        public string ErrorCode { get; set; }
    }

    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class OrderItem
    {
        public string product { get; set; }
        public int quantity { get; set; }
        public decimal priceEach { get; set; }
    }

    public class Order
    {
        public int orderNumber { get; set; }
        public string orderDate { get; set; }
        public string deliveryAddress { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public string deliveryExpected { get; set; }
    }

}
