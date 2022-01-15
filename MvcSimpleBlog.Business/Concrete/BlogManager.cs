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
    public class BlogManager : IBlogService
    {
        private IBlogDal blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            this.blogDal = blogDal;
        }

        public void Add(Blog blog)
        {
            blogDal.Add(blog);
        }

        public void Delete(Blog blog)
        {
            blogDal.Delete(blog);
        }

        public List<Blog> GetAll()
        {
            return blogDal.GetAllDetails();
        }

        public List<Blog> GetByCategoryId(int categoryId)
        {
            return blogDal.GetAllDetails(b => b.CategoryId == categoryId);
        }

        public Blog GetById(int id)
        {
            return blogDal.Get(b => b.Id == id);
        }

        public Blog GetBySeoUrl(string seroUrl)
        {
            return blogDal.GetDetails(b => b.SeoUrl == seroUrl);
        }

        public List<Blog> GetByUserId(int userId)
        {
            return blogDal.GetAllDetails(b => b.UserId == userId);
        }

        public void Update(Blog blog)
        {
            blogDal.Update(blog);
        }
    }
}
