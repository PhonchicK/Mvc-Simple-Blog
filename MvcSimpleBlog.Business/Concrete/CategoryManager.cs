using Core.Aspects.Postsharp.ValidationAspects;
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
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        [FluentValidationAspect(typeof(CategoryValidator))]
        public void Add(Category category)
        {
            categoryDal.Add(category);
        }

        public void Delete(Category category)
        {
            categoryDal.Delete(category);
        }

        public List<Category> GetAll(int page = 1, int itemPerPage = 10)
        {
            return categoryDal.GetList().Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }

        public Category GetById(int id)
        {
            return categoryDal.Get(c => c.Id == id);
        }

        [FluentValidationAspect(typeof(CategoryValidator))]
        public void Update(Category category)
        {
            categoryDal.Update(category);
        }
    }
}
