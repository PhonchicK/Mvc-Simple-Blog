using Core.Aspects.Postsharp.ValidationAspects;
using Core.Utilities.Security.Hashing;
using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Business.ValidationRules.FluentValidation;
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

        [FluentValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
            userDal.Add(user);
        }

        public void Delete(User user)
        {
            userDal.Delete(user);
        }

        public List<User> GetAll(int page = 1, int itemPerPage = 10)
        {
            return userDal.GetList().Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }

        public User GetById(int id)
        {
            return userDal.Get(u => u.Id == id);
        }

        public User Login(string username, string password)
        {
            password = HashingHelper.MD5Hash(password);
            return userDal.Get(u => u.Username == username && u.Password == password);
        }

        [FluentValidationAspect(typeof(UserValidator))]
        public void Update(User user)
        {
            userDal.Update(user);
        }
    }
}
