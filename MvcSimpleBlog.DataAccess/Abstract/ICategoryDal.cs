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
    public interface ICategoryDal : IEntityRepository<Category>
    {
        List<Category> GetAllDetails(Expression<Func<Category, bool>> filter = null);
        Category GetDetails(Expression<Func<Category, bool>> filter);
    }
}
