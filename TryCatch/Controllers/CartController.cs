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

        [HttpGet]
        public async Task<JsonResult> GetJson()
        {
            return Json(await GetCart(), JsonRequestBehavior.AllowGet);
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
        
        public async Task<ActionResult> Items(string guid)
        {
            var cart = await WebApiClient3.Instance.GetAsync<Cart>(string.Format("api/Cart/{0}", guid));
            return View(cart.Items);
        }

        [HttpPost]
        public async Task AddItem(int articleId, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                    await GetCart();

                var url = string.Format("api/Cart/{0}/Items/{1}/{2}",
                    HttpContext.Session["CartGuid"] as string, articleId, quantity);

                await WebApiClient3.Instance.PostAsync<object, Cart>(url, null);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at add article {0} to the cart: {1}", articleId, ex.Message));
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddItemJson(int articleId, int quantity)
        {
            await this.AddItem(articleId, quantity);
            return Json(string.Empty);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveItemJson(int articleId, int quantity)
        {
            await this.RemoveItem(articleId, quantity);
            return Json(string.Empty);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveItem(int articleId, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                    await GetCart();

                var url = string.Format("api/Cart/{0}/Items/{1}/{2}",
                    HttpContext.Session["CartGuid"] as string, articleId, quantity);

                await WebApiClient3.Instance.DeleteAsync(url);

                return PartialView("~/Views/Cart/_CartItemsGridPartial.cshtml", await GetCart());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at remove article {0} of the cart: {1}", articleId, ex.Message));
            }
        }

        public async Task<ActionResult> ConfirmCheckout()
        {
            var cart = await GetCart();
            return View(cart);
        }
        
        [Authorize]
        public async Task<ActionResult> Checkout()
        {
            var cart = await GetCart();

            var url = string.Format("api/Cart/{0}/Checkout", cart.Guid);
            var order = await WebApiClient3.Instance.PostAsync<object, Order>(url, null);
            // Clear the atual cart
            HttpContext.Session["CartGuid"] = string.Empty;
            RedirectToAction("Details", "Order", new { id = order.Id });
            return View(order);
        }
    }
}
