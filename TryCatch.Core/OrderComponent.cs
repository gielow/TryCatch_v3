using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;
using System.Data.Entity;

namespace TryCatch.Core
{
    public class OrderComponent : IOrderComponent
    {
        IDbRepository _repository;

        public OrderComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public Order Get(int id)
        {
            return _repository.Orders.Include(o => o.Customer).Include(o => o.Items).FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetMany()
        {
            return _repository.Orders.Include(o => o.Customer).Include(o => o.Items).ToList();
        }
    }
}
