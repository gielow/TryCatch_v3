using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
