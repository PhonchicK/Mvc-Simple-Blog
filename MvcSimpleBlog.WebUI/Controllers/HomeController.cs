using MvcSimpleBlog.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSimpleBlog.WebUI.Controllers
{
    [RouteArea("")]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private IBlogService blogService;
        private ICategoryService categoryService;
        private IUserService userService;
        public HomeController(IBlogService blogService, ICategoryService categoryService, IUserService userService)
        {
            this.blogService = blogService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        [Route("{page:int?}")]
        public ActionResult Index(int page = 1)
        {
            return View(blogService.GetAll(page));
        }

        [Route("{category}/{page:int?}")]
        public ActionResult Index(string category, int page = 1)
        {
            return View(blogService.GetByCategorySeoUrl(category, page));
        }

        [Route("details/{seoUrl}")]
        public ActionResult Details(string seoUrl)
        {
            var result = blogService.GetBySeoUrl(seoUrl);
            return View(result);
        }

        [Route("categories")]
        public ActionResult Categories()
        {
            return View(categoryService.GetAll(1, categoryService.CategoryCount()));
        }



        [ChildActionOnly]
        public PartialViewResult CategoriesMenu()
        {
            return PartialView(categoryService.GetAll());
        }
    }
}