using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.DataAccess.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using Core.Aspects.Postsharp.ValidationAspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSimpleBlog.Business.ValidationRules.FluentValidation;

namespace MvcSimpleBlog.Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private IBlogDal blogDal;
        public BlogManager(IBlogDal blogDal)
        {
            this.blogDal = blogDal;
        }

        [FluentValidationAspect(typeof(BlogValidator))]
        public void Add(Blog blog)
        {
            blogDal.Add(blog);
        }

        [FluentValidationAspect(typeof(BlogValidator))]
        public void Update(Blog blog)
        {
            blogDal.Update(blog);
        }

        public void Delete(Blog blog)
        {
            blogDal.Delete(blog);
        }

        public List<Blog> GetAll(int page = 1, int itemPerPage = 10)
        {
            return blogDal.GetAllDetails().OrderByDescending(b => b.Date).Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }

        public List<Blog> GetByCategoryId(int categoryId, int page = 1, int itemPerPage = 10)
        {
            return blogDal.GetAllDetails(b => b.CategoryId == categoryId).OrderByDescending(b => b.Date).Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }

        public Blog GetById(int id)
        {
            return blogDal.GetDetails(b => b.Id == id);
        }

        public Blog GetBySeoUrl(string seroUrl)
        {
            return blogDal.GetDetails(b => b.SeoUrl == seroUrl);
        }

        public List<Blog> GetByUserId(int userId, int page = 1, int itemPerPage = 10)
        {
            return blogDal.GetAllDetails(b => b.UserId == userId).OrderByDescending(b => b.Date).Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }

        public int BlogCount()
        {
            return blogDal.Count();
        }

        public List<Blog> GetByCategorySeoUrl(string categorySeoUrl, int page = 1, int itemPerPage = 10)
        {
            return blogDal.GetAllDetails(b => b.Category.SeoUrl == categorySeoUrl).OrderByDescending(b => b.Date).Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        }
    }
}
