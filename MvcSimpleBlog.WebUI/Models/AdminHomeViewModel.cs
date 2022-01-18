using MvcSimpleBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSimpleBlog.WebUI.Models
{
    public class AdminHomeViewModel
    {
        public int TotalBlogs { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
        public List<Blog> LatestBlogs { get; set; }
    }
}