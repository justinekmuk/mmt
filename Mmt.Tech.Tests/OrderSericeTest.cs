using Mmt.Tech.Data;
using Mmt.Tech.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Mmt.Tech.Tests
{
    public class OrderSericeTest
    {
        [Fact]
        public async Task GetRecentCustomerOrder_When_CustomerId_Not_Match_WithEmail()
        {
            var email = "cat.owner@mmtdigital.co.uk";
            var customerId = "A12345";
            //given
            var orderdetail = new CustomerDetail()
            {
                email = "cat.owner@mmtdigital.co.uk",
                customerId = "C34454",
                firstName = "Charlie",
                lastName = "Cat"
            };

            var custmerOrder = new CustomerOrder
            {
                OrderNumber = 1,
                OrderDate = DateTime.Now,
                deliveryExpected = DateTime.Now.AddDays(7),
                CustomerId = "A12345",
                OrderItems = new List<CustomerOrderItem>()
                {
                    new CustomerOrderItem
                    {
                         PriceEach = 2,
                         Product = "test",
                         Quantity = 1
                    }
                }
            };


            var mockCustomerOrderDetailservice  = new Mock<ICustomerOrderDetailService>();
            mockCustomerOrderDetailservice.Setup(c => c.GetCustomerOrderDeatil(email)).ReturnsAsync(orderdetail);

            var mockCustomerOrderData = new Mock<ICustomerOrderData>();
            mockCustomerOrderData.Setup(c => c.GetRecentOrder(customerId)).ReturnsAsync(custmerOrder);
            IMapper mapper = new Mapper();
            OrderService sut = new OrderService(mockCustomerOrderData.Object, mockCustomerOrderDetailservice.Object, mapper);
            var result = await sut.GetRecentCustomerOrder(email, customerId);

            mockCustomerOrderDetailservice.Verify(c => c.GetCustomerOrderDeatil(email), Times.Once);
            mockCustomerOrderData.Verify(c => c.GetRecentOrder(customerId), Times.Never);

            Assert.Equal( "INVALIDCUSTOMER", result.ErrorCode);
        }

        [Fact]
        public async Task GetRecentCustomerOrder_When_OrderDetail_isEmpty()
        {
            var email = "cat.owner@mmtdigital.co.uk";
            var customerId = "C34454";
            //given
            var orderdetail = new CustomerDetail()
            {
                email = "cat.owner@mmtdigital.co.uk",
                customerId = "C34454",
                firstName = "Charlie",
                lastName = "Cat"
            };

             


            var mockCustomerOrderDetailservice = new Mock<ICustomerOrderDetailService>();
            mockCustomerOrderDetailservice.Setup(c => c.GetCustomerOrderDeatil(email)).ReturnsAsync(orderdetail);

            var mockCustomerOrderData = new Mock<ICustomerOrderData>();
            mockCustomerOrderData.Setup(c => c.GetRecentOrder(customerId)).ReturnsAsync(new CustomerOrder());
            IMapper mapper = new Mapper();
            OrderService sut = new OrderService(mockCustomerOrderData.Object, mockCustomerOrderDetailservice.Object, mapper);
            var result = await sut.GetRecentCustomerOrder(email, customerId);

            mockCustomerOrderDetailservice.Verify(c => c.GetCustomerOrderDeatil(email), Times.Once);
            mockCustomerOrderData.Verify(c => c.GetRecentOrder(customerId), Times.Once);

            Assert.Equal(string.Empty, result.ErrorCode);
        }
    }
}
