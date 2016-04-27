using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface ICartComponent
    {
        Order Checkout(string guid, string userName);
        Order Checkout(Cart cart, Customer customer);
        Cart Get(string guid);
        Cart New();
        void AddItem(string guid, int articleId, int quantity);
        void RemoveItem(string guid, int articleId, int quantity);
    }
}
