using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
            // Create the order
            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            _repository.Orders.Add(order);
            
            // Just to the EF do not delete the items of the order
            //_repository.Entry<Cart>(cart).State = EntityState.Deleted;
            
            _repository.SaveChanges();

            return order;
        }

        public Cart Get(string guid)
        {
            return _repository.Carts.Include(ca => ca.Items).FirstOrDefault(c => c.Guid.Equals(guid));
        }

        public void AddItem(string guid, int articleId, int quantity)
        {
            var cart = _repository.Carts.Include(c => c.Items).FirstOrDefault(c => c.Guid == guid);
            var article = _xmlRepository.Articles.FirstOrDefault(a => a.Id == articleId);
            var cartItem = cart.Items.FirstOrDefault(i => i.ArticleId == articleId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var orderItem = new OrderItem();
                orderItem.ArticleId = article.Id;
                orderItem.ArticleName = article.Description;
                orderItem.ArticlePrice = article.Price;
                orderItem.CartGuid = guid;
                orderItem.Quantity = quantity;

                cart.Items.Add(orderItem);
            }

            _repository.Carts.Attach(cart);
            _repository.SaveChanges();
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
            var cart = _repository.Carts.FirstOrDefault(c => c.Guid == guid);
            var article = _xmlRepository.Articles.FirstOrDefault(a => a.Id == articleId);
            var cartItem = cart.Items.FirstOrDefault(i => i.ArticleId == articleId);

            if (cartItem != null)
            {
                cartItem.Quantity -= quantity;

                if (cartItem.Quantity < 0)
                    cart.Items.Remove(cartItem);
            }
            
            _repository.Carts.Attach(cart);
            _repository.SaveChanges();
        }
    }
}
