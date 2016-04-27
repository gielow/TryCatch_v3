using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class CartComponent: ICartComponent
    {
        private IDbRepository _repository;
        private IXmlRepository _xmlRepository;

        public CartComponent(IDbRepository repository, IXmlRepository xmlRepository)
        {
            _repository = repository;
            _xmlRepository = xmlRepository;
        }

        public Cart New()
        {
            var guid = Guid.NewGuid().ToString();
            _repository.Carts.Add(new Cart(guid));
            _repository.SaveChanges();

            return _repository.Carts.First(c => c.Guid.Equals(guid));
        }
        
        public Order Checkout(Cart cart, Customer customer)
        {
            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            _repository.Orders.Add(order);
            _repository.Carts.Remove(cart);
            _repository.SaveChanges();

            return order;
        }

        public Cart Get(string guid)
        {
            return _repository.Carts.FirstOrDefault(c => c.Guid.Equals(guid));
        }

        public void AddItem(string guid, int articleId, int quantity)
        {
            var cart = this.Get(guid);
            var article = _xmlRepository.Articles.FirstOrDefault(a => a.Id == articleId);
            
            cart.AddArticle(article, quantity);
        }

        public void Assign(Cart cart, Customer customer)
        {

        }

        public Order Checkout(string guid, string userName)
        {
            var cart = Get(guid);
            var customer = _repository.Customers.FirstOrDefault(c => c.Email == userName);

            return Checkout(cart, customer);
        }

        public void RemoveItem(string guid, int articleId, int quantity)
        {
            var cart = this.Get(guid);
            
            cart.RemoveArticle(articleId, quantity);
        }
    }
}
