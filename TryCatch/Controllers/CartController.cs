using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Controllers
{
    public class CartController : Controller
    {
        public async Task<ActionResult> New()
        {
            var cart = await WebApiClient3.Instance.PostAsync<object, Cart>("api/Cart/New", null);

            return View(cart);
        }

        // GET: Cart
        public async Task<ActionResult> Index()
        {
            return View(await GetCart());
        }

        private async Task<Cart> GetCart()
        {
            var guid = HttpContext.Session["CartGuid"] as string;
            Cart cart = null;
            // If there is a guid, try to load the cart
            if (!string.IsNullOrEmpty(guid))
                cart = await WebApiClient3.Instance.GetAsync<Cart>(string.Format("api/Cart/{0}", guid));
            // In case of the cart doesn't exists anymore
            if (cart == null)
                cart = await WebApiClient3.Instance.PostAsync<object, Cart>("api/Cart/New", null);

            HttpContext.Session["CartGuid"] = cart.Guid;

            return cart;
        }

        // GET: Cart/Details/5
        public async Task<ActionResult> Details(string guid)
        {
            return await Index();
        }
        
        public ActionResult Items(string guid)
        {
            var cart = WebApiClient3.Instance.GetAsync<Cart>(string.Format("api/Cart/{0}", guid));
            return View(cart.Result.Items);
        }

        [HttpPost]
        public async Task AddItem(int articleId, int quantity)
        {
            if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                await GetCart();

            var url = string.Format("api/Cart/{0}/Items/{1}/{2}",
                HttpContext.Session["CartGuid"] as string, articleId, quantity);
            
            await WebApiClient3.Instance.PostAsync<object, Cart>(url, null);
        }

        [HttpDelete]
        public async Task RemoveItem(int articleId, int quantity)
        {
            if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                await GetCart();

            var url = string.Format("api/Cart/{0}/Items/{1}/{2}",
                HttpContext.Session["CartGuid"] as string, articleId, quantity);

            await WebApiClient3.Instance.DeleteAsync(url);
        }

        [Authorize]
        public async Task<ActionResult> Checkout()
        {
            var cart = await GetCart();

            var url = string.Format("api/Cart/{0}/Checkout", cart.Guid);
            var order = await WebApiClient3.Instance.PostAsync<object, Order>(url, null);

            RedirectToAction("Details", "Order", new { id = order.Id });
            return View(order);
        }
    }
}
