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
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name can't be empty.")
                .MinimumLength(2).WithMessage("Category name must be longer than 2 character.")
                .MaximumLength(50).WithMessage("Category name must be shorter than 50 character.");

            RuleFor(x => x.SeoUrl).NotEmpty().WithMessage("Category Seo Url can't be null.")
                .MinimumLength(2).WithMessage("Category Seo Url must be longer than 2 character.")
                .MaximumLength(50).WithMessage("Category Seo Url must be shorter than 50 character.");

            RuleFor(x => x).Must(SeoUrlControl).WithMessage("Category name must be unique.");
        }
        private bool SeoUrlControl(Category category)
        {
            using (BlogContext context = new BlogContext())
            {
                if (context.Categories.SingleOrDefault(x => x.SeoUrl == category.SeoUrl && x.Id != category.Id) == null)
                    return true;
            }
            return false;
        }
    }
}
