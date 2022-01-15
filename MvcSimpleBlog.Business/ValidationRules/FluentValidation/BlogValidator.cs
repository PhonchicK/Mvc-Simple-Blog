using FluentValidation;
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
            RuleFor(x => x.SeoUrl).NotEmpty().WithMessage("Seo Url boş olamaz");
        }
    }
}
