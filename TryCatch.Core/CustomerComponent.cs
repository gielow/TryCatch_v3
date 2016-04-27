using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class CustomerComponent : ICustomerComponent
    {
        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Put(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool ValidateLogin(CustomerLoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
