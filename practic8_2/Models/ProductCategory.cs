using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic8_2.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
