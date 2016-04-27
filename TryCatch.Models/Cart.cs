using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<OrderItem>();
        }

        public Cart(string guid)
        {
            Guid = guid;
            Items = new List<OrderItem>();
        }

        [Key]
        [Required]
        public string Guid { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Total
        {
            get
            {
                return Items.Sum(i => i.Total);
            }
        }

        public override bool Equals(object obj)
        {
            var obj2 = obj as Cart;
            if (obj2 == null)
                return false;

            return this.Guid.Equals(obj2.Guid);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
