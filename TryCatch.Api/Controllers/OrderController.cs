using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return _component.GetMany().AsQueryable();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            var order = _component.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}