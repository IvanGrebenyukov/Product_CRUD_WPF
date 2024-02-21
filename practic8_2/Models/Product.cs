using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic8_2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        [Precision(14, 2)]
        public double Price { get; set; }
        [Precision(14, 2)]
        public double? Discount { get; set; }

        public int CategoryId { get; set; } 

        [ForeignKey("CategoryId")]
        public ProductCategory? ProductCategory { get; set; }
    }
}
