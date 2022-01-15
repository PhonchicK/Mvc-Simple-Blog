using Core.DataAccess;
using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.DataAccess.Abstract
{
    public interface IBlogDal : IEntityRepository<Blog>
    {
        List<Blog> GetAllDetails(Expression<Func<Blog, bool>> filter = null);
        Blog GetDetails(Expression<Func<Blog, bool>> filter);
    }
}
