using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using TryCatch.Api.Models;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Api.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        IOrderComponent _component;

        public OrderController(IOrderComponent component)
        {
            _component = component;
        }

        // GET: api/Orders
        public IQueryable<Order> GetOrders()
        {
            var customerId = Convert.ToInt32((User.Identity as ClaimsIdentity).Claims.First(c => c.Type.Equals("CustomerId")).Value);
            var orders = _component.GetMany();
            //var orders = ;
            return orders.Where(o => o.Customer.Id == customerId).AsQueryable();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)

        {
            var customerId = Convert.ToInt32((User.Identity as ClaimsIdentity).Claims.First(c => c.Type.Equals("CustomerId")).Value);
            var order = _component.Get(id);

            if (order == null || order.Customer.Id != customerId)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}