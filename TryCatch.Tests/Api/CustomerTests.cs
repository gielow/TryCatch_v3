using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryCatch.Models;

namespace TryCatch.Tests.Api
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void Create()
        {
            var customer = new Customer();
            customer.FirstName = "André";
            customer.LastName = "Gielow";
            customer.HouseNumber = 291;
            customer.Password = "Test123_";
            customer.Title = "Mr.";
            customer.ZipCode = "89057-036";
            customer.Email = "andre.gielow@gmail.com";
            customer.City = "Blumenau";
            customer.Address = "Primavera st.";
        }
    }
}
