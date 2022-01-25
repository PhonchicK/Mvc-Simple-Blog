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
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter uzunluğunda olabilir");
            RuleFor(x => x).Must(SeoUrlControl).WithMessage("Aynı Seo Url i kullanan bir blog bulunuyor");
            RuleFor(x => x.SeoUrl).NotEmpty().WithMessage("Seo Url boş olamaz");
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
