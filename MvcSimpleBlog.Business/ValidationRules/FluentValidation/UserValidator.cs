using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MvcSimpleBlog.DataAccess.Concrete.EntityFramework;
using MvcSimpleBlog.Entities.Concrete;

namespace MvcSimpleBlog.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User name is can't be empty.")
                .MinimumLength(2).WithMessage("User name must be longer than 2 character.")
                .MaximumLength(25).WithMessage("User name must be shorter than 25 character.");
                //.Matches(@"^[a-zA-Z-']*$").WithMessage("User name containable just alphabetic characters and numbers.");

            RuleFor(x => x).Must(UsernameControl).WithMessage("User name must be unique.");

            RuleFor(x => x.Email).EmailAddress();
        }
        private bool UsernameControl(User user)
        {
            using (BlogContext context = new BlogContext())
            {
                if (context.Users.SingleOrDefault(x => x.Username == user.Username && x.Id != user.Id) == null)
                    return true;
            }
            return false;
        }
    }
}
