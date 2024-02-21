using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic8_1_grebenukov.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [MaxLength(100)]
        public string PostName { get; set; }
        public string PostText { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsPublished { get; set; }

        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

    }
}
