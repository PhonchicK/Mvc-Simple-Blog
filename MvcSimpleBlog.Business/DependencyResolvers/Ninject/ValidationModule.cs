using FluentValidation;
using MvcSimpleBlog.Business.ValidationRules.FluentValidation;
using MvcSimpleBlog.Entities.Concrete;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.DependencyResolvers.Ninject
{
    public class ValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<Blog>>().To<BlogValidator>().InSingletonScope();
            Bind<IValidator<Category>>().To<CategoryValidator>().InSingletonScope();
            Bind<IValidator<User>>().To<UserValidator>().InSingletonScope();
        }
    }
}
