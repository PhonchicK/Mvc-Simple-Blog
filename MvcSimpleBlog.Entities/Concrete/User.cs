using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSimpleBlog.Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            Blogs = new List<Blog>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int Auth { get; set; }

        public virtual List<Blog> Blogs { get; set; }
    }
}
