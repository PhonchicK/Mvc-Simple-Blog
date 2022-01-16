using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Business.Abstract
{
    public interface IBlogService
    {
        List<Blog> GetAll(int page = 1);
        List<Blog> GetByCategoryId(int categoryId, int page = 1);
        List<Blog> GetByUserId(int userId, int page = 1);
        Blog GetById(int id);
        Blog GetBySeoUrl(string seroUrl);
        void Add(Blog blog);
        void Update(Blog blog);
        void Delete(Blog blog);
    }
}
