using Mmt.Tech.Data;
using System;
using System.Threading.Tasks;

namespace Mmt.Tech.Services
{
    public class OrderService  : IOrderService
    {
        private readonly ICustomerOrderData customerOrder;
        private readonly ICustomerOrderDetailService customerOrderDetail;
        private readonly IMapper mapper;


        public OrderService(ICustomerOrderData customerOrder,ICustomerOrderDetailService customerOrderDetail, IMapper mapper)
        {
            this.customerOrder = customerOrder;
            this.customerOrderDetail = customerOrderDetail;
            this.mapper = mapper;
        }

        public async Task<RecentCustomerOrder> GetRecentCustomerOrder(string email, string customerId)
        {
            var customerdetail = await customerOrderDetail.GetCustomerOrderDeatil(email);
            if(customerdetail.customerId != customerId)
            {
                var errorOrder = new RecentCustomerOrder();
                errorOrder.ErrorCode = "INVALIDCUSTOMER";
                return errorOrder;
            };
            var order = await customerOrder.GetRecentOrder(customerId);
            var recentOrder = mapper.MapToRecentCustomerOrder(customerdetail, order);
            recentOrder.ErrorCode = string.Empty;
            return recentOrder;
        }
    }
}
