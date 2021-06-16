using Mmt.Tech.Data;

namespace Mmt.Tech.Services
{
    public interface IMapper
    {
        RecentCustomerOrder MapToRecentCustomerOrder(CustomerDetail customerDetail, CustomerOrder customerOrder);
    }
}
