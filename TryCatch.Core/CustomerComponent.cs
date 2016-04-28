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
        IDbRepository _repository;

        public CustomerComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public Customer Get(int id)
        {
            return _repository.Customers.Find(id);
        }

        public List<Customer> GetMany()
        {
            return _repository.Customers.ToList();
        }

        public Customer Put(Customer customer)
        {
            if (customer.Id > 0)
                _repository.Customers.Attach(customer);
            else
            {
                if (_repository.Customers.Count(c => c.Email == customer.Email) > 0)
                    throw new Exception(string.Format("The e-mail '{0}' already exists", customer.Email));

                _repository.Customers.Add(customer);
            }

            _repository.SaveChanges();

            return customer;
        }

        public Customer ValidateLogin(CustomerLoginModel loginModel)
        {
            return _repository.Customers.FirstOrDefault(c => c.Email == loginModel.Email && c.Password == loginModel.Password);
        }
    }
}
