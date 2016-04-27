using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Tests.Api
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void GetNewCart()
        {
            //var cart = WebApiClient.Instance.GetAsync<Cart>("api/Cart/New");

            var cart = WebApiClient.Instance.GetAsync("api/Cart/New", "");
        }
    }
}
