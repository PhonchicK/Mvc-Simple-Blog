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
    public class EfCategoryDal : EfEntityRepositoryBase<Category, BlogContext>, ICategoryDal
    {
        public List<Category> GetAllDetails(Expression<Func<Category, bool>> filter = null)
        {
            using (BlogContext context = new BlogContext())
            {
                return filter == null ? context.Categories.Include("Blogs").ToList()
                    : context.Categories.Include("Blogs").Where(filter).ToList();
            }
        }

        public Category GetDetails(Expression<Func<Category, bool>> filter)
        {
            using (BlogContext context = new BlogContext())
            {
                return context.Categories.Include("Blogs").SingleOrDefault(filter);
            }
        }
    }
}
