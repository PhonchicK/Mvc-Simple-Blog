﻿using MvcSimpleBlog.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSimpleBlog.WebUI.Controllers
{
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
        public ActionResult Index(int page = 1)
        {
            return View(blogService.GetAll(page));
        }
        public ActionResult Details(string seoUrl)
        {
            var result = blogService.GetBySeoUrl(seoUrl);
            return View(result);
        }
        [ChildActionOnly]
        public PartialViewResult CategoriesMenu()
        {
            return PartialView(categoryService.GetAll());
        }
    }
}