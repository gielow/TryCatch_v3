using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Data
{
    public class DatabaseDataProvider : DbContext, IDbRepository
    {
        public DatabaseDataProvider() : base(System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"])
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static DatabaseDataProvider Create()
        {
            return new DatabaseDataProvider();
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    
        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
