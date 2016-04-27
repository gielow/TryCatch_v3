using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface ICustomerComponent
    {
        Customer Get(int id);
        List<Customer> GetMany();
        Customer Put(Customer customer);
        Customer ValidateLogin(CustomerLoginModel loginModel);
    }
}
