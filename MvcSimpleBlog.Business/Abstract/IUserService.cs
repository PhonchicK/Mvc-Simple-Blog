using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll(int page = 1, int itemPerPage = 10);
        User GetById(int id);
        User Login(string username, string password);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
