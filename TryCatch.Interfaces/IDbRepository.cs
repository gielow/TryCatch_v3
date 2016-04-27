using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface IDbRepository
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Order> Orders { get;  }

        DbSet<OrderItem> OrderItems { get; }
        DbSet<Customer> Customers { get;  }

        DbSet<Cart> Carts { get; }
    }
}
