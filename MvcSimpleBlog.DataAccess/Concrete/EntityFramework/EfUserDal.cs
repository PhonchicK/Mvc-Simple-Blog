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
    public class EfUserDal : EfEntityRepositoryBase<User, BlogContext>, IUserDal
    {
        public List<User> GetAllDetails(Expression<Func<User, bool>> filter = null)
        {
            using (BlogContext context = new BlogContext())
            {
                return filter == null ? context.Users.Include("Blogs").ToList()
                    : context.Users.Include("Blogs").Where(filter).ToList();
            }
        }

        public User GetDetails(Expression<Func<User, bool>> filter)
        {
            using (BlogContext context = new BlogContext())
            {
                return context.Users.Include("Blogs").SingleOrDefault(filter);
            }
        }
    }
}
