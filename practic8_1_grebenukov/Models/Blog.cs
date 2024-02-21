using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic8_1_grebenukov.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        [MaxLength(100)]
        public string BlogName { get; set; }

        [RegularExpression(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,}$")]
        public string AuthorEmail { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateCreation { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
