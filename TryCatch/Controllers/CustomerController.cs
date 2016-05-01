using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TryCatch.Models;

namespace TryCatch.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public async Task<ActionResult> Create(Customer model, string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                await WebApiClient3.Instance.PostAsync<Customer>("api/Customer", model);

                var loginModel = new CustomerLoginModel()
                {
                    Email = model.Email,
                    Password = model.Password
                };

                await this.Login(loginModel, returnUrl);
                
                return RedirectToLocal(returnUrl);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(CustomerLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginOk = await ValidateLogin(model);

                if (loginOk)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private async Task<bool> ValidateLogin(CustomerLoginModel model)
        {
            var res = await WebApiClient3.Instance.AuthenticateAsync(model.Email, model.Password);

            if (res)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                Response.Cookies.Add(new HttpCookie("AuthToken", WebApiClient3.Instance.AuthToken));
                
            }

            return res;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            WebApiClient3.Instance.AuthToken = string.Empty;
            Response.SetCookie(new HttpCookie("AuthToken", string.Empty));
            Response.Cookies.Remove("AuthToken");


            return RedirectToAction("Index", "Article");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Article");
            }
        }
    }
}
