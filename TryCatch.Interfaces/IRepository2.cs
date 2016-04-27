using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface IRepository2
    {
        List<Article> Articles { get;  }
        DbSet<Order> Orders { get;  }
        DbSet<Customer> Customers { get;  }

        DbSet<Cart> Carts { get; }
    }
}
