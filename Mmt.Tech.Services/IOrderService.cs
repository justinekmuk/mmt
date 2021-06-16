using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mmt.Tech.Services
{
    public interface IOrderService
    {
        Task<RecentCustomerOrder> GetRecentCustomerOrder(string email, string customerId);
    }
}
