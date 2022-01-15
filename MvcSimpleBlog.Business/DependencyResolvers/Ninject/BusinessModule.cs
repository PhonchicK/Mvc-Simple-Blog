using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Business.Concrete;
using MvcSimpleBlog.DataAccess.Abstract;
using MvcSimpleBlog.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogService>().To<BlogManager>().InSingletonScope();
            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();

            Bind<IBlogDal>().To<EfBlogDal>();
            Bind<IUserDal>().To<EfUserDal>();
            Bind<ICategoryDal>().To<EfCategoryDal>();

            Bind<DbContext>().To<BlogContext>();
        }
    }
}
