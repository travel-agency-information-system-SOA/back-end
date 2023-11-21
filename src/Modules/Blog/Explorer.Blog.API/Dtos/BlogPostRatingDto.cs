using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogPostRatingDto
    {
        public bool IsPositive { get; set; }
        public DateTime CreationTime { get; set; }
        public int UserId { get; set; }
    }
}
