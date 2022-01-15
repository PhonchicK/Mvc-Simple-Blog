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
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            categoryDal.Add(category);
        }

        public void Delete(Category category)
        {
            categoryDal.Delete(category);
        }

        public List<Category> GetAll()
        {
            return categoryDal.GetList();
        }

        public Category GetById(int id)
        {
            return categoryDal.Get(c => c.Id == id);
        }

        public void Update(Category category)
        {
            categoryDal.Update(category);
        }
    }
}
