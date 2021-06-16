using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mmt.Tech.Services
{
    public interface ICustomerOrderDetailService
    {
        Task<CustomerDetail> GetCustomerOrderDeatil(string email);
    }
}
