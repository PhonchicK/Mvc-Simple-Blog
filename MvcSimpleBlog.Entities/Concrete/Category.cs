using ETicaretDersiProje.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Entities.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            Blogs = new List<Blog>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoUrl { get; set; }

        public virtual List<Blog> Blogs { get; set; }
    }
}
