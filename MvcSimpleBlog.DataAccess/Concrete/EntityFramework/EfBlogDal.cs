using Core.DataAccess.EntityFramework;
using MvcSimpleBlog.DataAccess.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog, BlogContext>, IBlogDal
    {
        public List<Blog> GetAllDetails(Expression<Func<Blog, bool>> filter = null)
        {
            using (BlogContext context = new BlogContext())
            {
                return filter == null ? context.Blogs.Include("Category").Include("User").ToList() : 
                    context.Blogs.Include("Category").Include("User").Where(filter).ToList();
            }
        }

        public Blog GetDetails(Expression<Func<Blog, bool>> filter)
        {
            using (BlogContext context = new BlogContext())
            {
                return context.Blogs.Include("Category").Include("User").SingleOrDefault(filter);
            }
        }
    }
}
