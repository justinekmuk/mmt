using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mmt.Tech.Services
{
    public class CustomerOrderDetailService : ICustomerOrderDetailService
    {
        //Todo : move the api key to appsettings.json 
        public readonly string apiKey = "1CrsOooSHlV15C7OYnLY0DHjBHyjzoI8LNHITV04cNCyNCahecPDhw==";
        private readonly HttpClient httpClient;

        public  CustomerOrderDetailService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomerDetail> GetCustomerOrderDeatil(string email)
        {
            var querystring = $"/api/GetUserDetails?code={apiKey}&email={email}";
            using (var result = await httpClient.GetAsync($"{querystring}")) //{apiUrl}
            {
               string content = await result.Content.ReadAsStringAsync();
               var customerDetail =   JsonConvert.DeserializeObject<CustomerDetail>(content);
               return customerDetail;
            }
        }
    }
}
