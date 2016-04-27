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
    public class CustomerController : ApiController
    {
        private ICustomerComponent _component;

        public CustomerController(ICustomerComponent component)
        {
            _component = component;
        }

        // GET: api/Customer
        public IQueryable<Customer> GetCustomer()
        {
            return _component.GetMany().AsQueryable();
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _component.Get(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("api/Customer/ValidateLogin")]
        public bool ValidateLogin(CustomerLoginModel model)
        {
            return _component.ValidateLogin(model) != null;
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _component.Put(customer);
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_component.Put(customer));
        }
    }
}