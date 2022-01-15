using Core.DataAccess;
using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
