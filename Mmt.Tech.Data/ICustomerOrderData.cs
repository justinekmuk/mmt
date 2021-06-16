using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mmt.Tech.Data
{
    public interface ICustomerOrderData
    {
        Task<CustomerOrder> GetRecentOrder(string customerID);
    }
}
