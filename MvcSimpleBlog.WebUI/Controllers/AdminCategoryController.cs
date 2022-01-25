using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using MvcSimpleBlog.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSimpleBlog.WebUI.Controllers
{
    [Auth]
    [RouteArea("admin")]
    [RoutePrefix("category")]
    public class AdminCategoryController : Controller
    {
        private ICategoryService categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [Route("page?")]
        public ActionResult Index(int page = 1)
        {
            return View(categoryService.GetAll(page));
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public ActionResult Create()
        {
            return View(new Category());
        }
    }
}