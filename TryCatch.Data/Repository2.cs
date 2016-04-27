using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Data
{
    public class Repository2 : IRepository2
    {
        public List<Article> Articles
        {
            get
            {
                return XmlDataProvider.Instance.Get<List<Article>>("articles.xml");
            }
        }

        public DbSet<Order> Orders
        {
            get
            {
                using(var tc = new DatabaseDataProvider())
                    return tc.Orders;
            }
        }

        public DbSet<Customer> Customers
        {
            get
            {
                using (var tc = new DatabaseDataProvider())
                    return tc.Customers;
            }
        }

        public DbSet<Cart> Carts
        {
            get
            {
                using (var tc = new DatabaseDataProvider())
                    return tc.Carts;
            }
        }
    }
}
