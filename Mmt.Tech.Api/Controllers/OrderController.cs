using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mmt.Tech.Data;
using Mmt.Tech.Services;

namespace Mmt.Tech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputModel model)
        {
            var recentOrder = await orderService.GetRecentCustomerOrder(model.user,model.customerId);
            
            if(recentOrder.ErrorCode == "INVALIDCUSTOMER")
                return BadRequest("Invalid Customer");

            return Ok(recentOrder);
        }

    }
}
