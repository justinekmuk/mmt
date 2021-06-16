using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mmt.Tech.Data
{
    public class CustomerOrderData : ICustomerOrderData
    {
        //Todo : get the connectstring from appsettings
        private readonly string connectionString = "Server=tcp:sse.database.windows.net,1433;Initial Catalog=SSE_Test;Persist Security Info=False;User ID=mmt-sse-test;Password=database-user-01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        /// <summary>
        ///   Todo: null and type cast Validation to be comepleted and also has check the cutomerId exits before making join query.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<CustomerOrder> GetRecentOrder(string customerID)
        {
            CustomerOrder order = null;

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "select Orders.OrderId,Orders.CustomerId,Orders.OrderDate,Orders.DeliveryExpected,products.ProductName,OrderItems.Price,OrderItems.Quantity " +
                    "from Orders inner join orderitems on Orders.OrderId = OrderItems.OrderId " +
                    "inner join products on products.productId = OrderItems.productId where Orders.OrderId = (select top 1 orderId from Orders where customerId=@customerId order by OrderDate desc)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    //command.Parameters.AddWithValue("@customerId", "R223232"); //XM45001
                    command.Parameters.AddWithValue("@customerId", customerID);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        order = new CustomerOrder();
                        List<CustomerOrderItem> orderitems = new List<CustomerOrderItem>();
                        if (await reader.ReadAsync())
                        {
                            order.OrderNumber = int.Parse(reader["ORDERID"].ToString());
                            order.CustomerId = reader["CustomerId"].ToString();
                            order.deliveryExpected = DateTime.Parse(reader["DeliveryExpected"].ToString());
                            var item = new CustomerOrderItem();
                            item.Product = reader["ProductName"].ToString();
                            item.Product = reader["Price"].ToString();
                            item.Product = reader["Quantity"].ToString();
                            orderitems.Add(item);

                        }
                        if (orderitems.Any()) order.OrderItems = orderitems;
                    }
                }
            }

            return order;
        }

    }
}
