using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogContext _context;
        private readonly DbSet<BlogPost> _blogPosts; //nisam sigurna DbSet?


        public BlogPostRepository(BlogContext context)
        {
            _context = context;
            _blogPosts = _context.BlogPosts;
        }
        public List<BlogPost> GetByAuthorId(int authorId)
        {
            return _context.BlogPosts.Where(bp => bp.AuthorId == authorId).ToList();
        }

    }
}
