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

        public List<Blog> GetAll(int page = 1)
        {
            return blogDal.GetAllDetails().Skip((page - 1) * 10).Take(10).ToList();
        }

        public List<Blog> GetByCategoryId(int categoryId, int page = 1)
        {
            return blogDal.GetAllDetails(b => b.CategoryId == categoryId).Skip((page - 1) * 10).Take(10).ToList();
        }

        public Blog GetById(int id)
        {
            return blogDal.Get(b => b.Id == id);
        }

        public Blog GetBySeoUrl(string seroUrl)
        {
            return blogDal.GetDetails(b => b.SeoUrl == seroUrl);
        }

        public List<Blog> GetByUserId(int userId, int page = 1)
        {
            return blogDal.GetAllDetails(b => b.UserId == userId).Skip((page - 1) * 10).Take(10).ToList();
        }
    }
}
