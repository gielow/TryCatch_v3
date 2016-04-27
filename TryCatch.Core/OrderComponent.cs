using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class OrderComponent : IOrderComponent
    {
        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetMany()
        {
            throw new NotImplementedException();
        }
    }
}
