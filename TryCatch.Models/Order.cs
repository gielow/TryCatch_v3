using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public enum OrderStatus
    {
        WaitingPayment,
        Processing,
        Sent,
        Delivered
    }

    public class Order
    {
        [Key]
        [Display(Name = "Protocol")]
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DateTime { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
    }
}
