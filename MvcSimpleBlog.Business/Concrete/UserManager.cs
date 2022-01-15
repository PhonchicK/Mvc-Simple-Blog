using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.DataAccess.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        public void Add(User user)
        {
            userDal.Add(user);
        }

        public void Delete(User user)
        {
            userDal.Delete(user);
        }

        public List<User> GetAll()
        {
            return userDal.GetList();
        }

        public User GetById(int id)
        {
            return userDal.Get(u => u.Id == id);
        }

        public void Update(User user)
        {
            userDal.Update(user);
        }
    }
}
