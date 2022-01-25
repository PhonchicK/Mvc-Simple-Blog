using FluentValidation;
using MvcSimpleBlog.DataAccess.Abstract;
using MvcSimpleBlog.DataAccess.Concrete.EntityFramework;
using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.ValidationRules.FluentValidation
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Blog title can't be empty.")
                .MaximumLength(50).WithMessage("Blog title must be shorter than 50 character.");

            RuleFor(x => x.SeoUrl).NotEmpty().WithMessage("Blog Seo Url can't be empty.");
            RuleFor(x => x).Must(SeoUrlControl).WithMessage("Blog Seo Url must be unique");
        }
        private bool SeoUrlControl(Blog blog)
        {
            using (BlogContext context = new BlogContext())
            {
                if (context.Blogs.SingleOrDefault(x => x.SeoUrl == blog.SeoUrl && x.Id != blog.Id) == null)
                    return true;
            }
            return false;
        }
    }
}
