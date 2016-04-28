using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Api.Controllers
{
    public class CartController : ApiController
    {
        ICartComponent _component;
        ICustomerComponent _customerComponent;

        public CartController(ICartComponent component, ICustomerComponent customerComponent)
        {
            _component = component;
            _customerComponent = customerComponent;
        }

        [HttpPost]
        [ResponseType(typeof(Cart))]
        [Route("api/Cart/New")]
        public IHttpActionResult New()
        {
            return Ok(_component.New());
        }

        //[HttpGet]
        [ResponseType(typeof(Cart))]
        [Route("api/Cart/{guid}")]
        public IHttpActionResult Get(string guid)
        {
            var cart = _component.Get(guid);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }
        
        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrderItem>))]
        [Route("api/Cart/{guid}/Items")]
        public IHttpActionResult Items(string guid)
        {
            var cart = _component.Get(guid);
            if (cart == null)
                return NotFound();

            return Ok(cart.Items);
        }

        [HttpPost]
        [Route("api/Cart/{guid}/Items/{articleId}/{quantity}")]
        public IHttpActionResult AddItem(string guid, int articleId, int quantity)
        {
            _component.AddItem(guid, articleId, quantity);
            
            return Ok();
        }

        [HttpDelete]
        [Route("api/Cart/{guid}/Items/{articleId}/{quantity}")]
        public IHttpActionResult RemoveItem(string guid, int articleId, int quantity)
        {
            _component.RemoveItem(guid, articleId, quantity);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("api/Cart/{guid}/Checkout")]
        public IHttpActionResult Checkout(string guid)
        {
            var customerId = Convert.ToInt32((User.Identity as ClaimsIdentity).Claims.First(c => c.Type.Equals("CustomerId")).Value);
            var customer = _customerComponent.Get(customerId);
            var cart = _component.Get(guid);
            var order = _component.Checkout(cart, customer);

            return Ok(order);
        }
    }
}
