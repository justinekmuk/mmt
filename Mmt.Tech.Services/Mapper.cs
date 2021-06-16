using Mmt.Tech.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mmt.Tech.Services
{
    //Todo: User AutoMapper for mapping objects
    public class Mapper : IMapper
    {
        public RecentCustomerOrder MapToRecentCustomerOrder(CustomerDetail customerDetail, CustomerOrder customerOrder)
        {
            RecentCustomerOrder recentCustomerOrder = new RecentCustomerOrder();

            var customer = new Customer
            {
                firstName = customerDetail.firstName,
                lastName  = customerDetail.lastName
            };

            var list = new List<OrderItem>();
            if (customerOrder.OrderItems != null)
            {
                foreach (var item in customerOrder.OrderItems)
                {
                    var orderitem = new OrderItem();
                    orderitem.product = item.Product;
                    orderitem.priceEach = item.PriceEach;
                    orderitem.quantity = item.Quantity;
                    list.Add(orderitem);
                };
            }

            var order = new Order
            {
               orderNumber = customerOrder.OrderNumber,
               orderDate = customerOrder.OrderDate.ToString(),
               deliveryAddress = $"{customerDetail.houseNumber}{ customerDetail.street} {customerDetail.lastName}",
               deliveryExpected = customerOrder.deliveryExpected.ToString(),
            };

            recentCustomerOrder.customer = customer;
            recentCustomerOrder.order = order;

            return recentCustomerOrder;
        }
    }
}
