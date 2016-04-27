using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Api.Controllers
{
    public class CartController : ApiController
    {
        ICartComponent _component;

        public CartController(ICartComponent component)
        {
            _component = component;
        }

        [HttpGet]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult New()
        {
            return Ok(_component.New());
        }

        [HttpGet]
        [ResponseType(typeof(Cart))]
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
        public IHttpActionResult Checkout(string guid)
        {
            var order = _component.Checkout(guid, User.Identity.Name);

            return Ok(order);
        }
    }
}
