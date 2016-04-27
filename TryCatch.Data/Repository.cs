using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Data;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Data
{
    public class Repository : IRepository
    {
        public List<Article> Articles
        {
            get
            {
                var articles = new List<Article>();
                articles.Add(new Article() {
                    Description = "Article 01",
                    Id = 1,
                    Price = 10
                });

                return articles;
            }
        }

        public List<Customer> Customers
        {
            get
            {
                var customers = new List<Customer>();

                customers.Add(new Customer() {
                    Address = "Primavera st.",
                    City = "Blumenau",
                   Email = "andre.gielow@gmail.com" ,
                   HouseNumber = 291,
                   FirstName = "André",
                   LastName = "Gielow",
                   Id = 1,
                   Password = "password",
                   Title = "Mr.",
                   ZipCode = "89057-036"
                });

                return customers;
            }
        }

        public List<Order> Orders
        {
            get
            {
                var orders = new List<Order>();

                orders.Add(new Order() {
                    Customer = this.Customers.First(),
                    Id = 1,
                    Items = new List<OrderItem>(),
                    Status = OrderStatus.Processing,
                    DateTime = DateTime.Now
                });

                return orders;
            }
        }
    }
}
