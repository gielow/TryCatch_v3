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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public async Task<ActionResult> Create(Customer model, string returnUrl)
        {
            try
            {
                //WebApiService.Instance.PostAsync<Customer>("Create", model);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost/TryCatchApi_v2/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsJsonAsync("api/Customer", model);

                    if (response.IsSuccessStatusCode)
                    {
                        // Get the URI of the created resource.
                        Uri gizmoUrl = response.Headers.Location;
                        var loginModel = new CustomerLoginModel() {
                            Email = model.Email,
                            Password = model.Password
                        };

                        this.Login(loginModel, returnUrl);
                    }
                    else
                    {
                        throw new Exception("Error at save customer");
                    }
                    //client.PostAsync<Customer>("http://localhost/TryCatchApi_v2/api/Customer/", model)
                }

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
        public ActionResult Login(CustomerLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateLogin(model.Email, model.Password).Result)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
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

        private async Task<bool> ValidateLogin(string email, string password)
        {
            var model = new CustomerLoginModel() { Email = email, Password = password };
            var res = WebApiService.Instance.PostAsync<CustomerLoginModel, bool>("api/Customer/ValidateLogin", model).Result;

            if (res)
                FormsAuthentication.SetAuthCookie(email, false);

            return res;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
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
