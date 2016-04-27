using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class OrderItem// : Record
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string CartGuid { get; set; }
        [Required]
        public int Quantity { get; set; }
        // Redundance of information because the Articles are stored in a diferent database
        [Required]
        public long ArticleId { get; set; }
        [Required]
        public string ArticleName { get; set; }
        [Required]
        public decimal ArticlePrice { get; set; }
        public decimal Total
        {
            get
            {
                return this.Quantity * ArticlePrice;
            }
        }
    }
}
