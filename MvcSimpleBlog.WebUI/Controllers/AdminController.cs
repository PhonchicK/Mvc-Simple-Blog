using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using MvcSimpleBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSimpleBlog.WebUI.Filters;

namespace MvcSimpleBlog.WebUI.Controllers
{
    [Auth]
    public class AdminController : Controller
    {
        private IBlogService blogService;
        private ICategoryService categoryService;
        private IUserService userService;

        public AdminController(IBlogService blogService, ICategoryService categoryService, IUserService userService)
        {
            this.blogService = blogService;
            this.categoryService = categoryService;
            this.userService = userService;
        }
        public ActionResult Index()
        {
            var model = new AdminHomeViewModel()
            {
                TotalBlogs = blogService.GetAll().Count,
                TotalCategories = categoryService.GetAll().Count,
                TotalUsers = userService.GetAll().Count,
                LatestBlogs = blogService.GetAll()
            };
            return View(model);
        }
        #region Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if(Session["user"] != null)
            {
                return RedirectToAction("Index");
            }
            return View(new LoginViewModel());
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                User user = userService.Login(model.Username, model.Password);
                Session["user"] = user;

                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
        #region Blogs
        public ActionResult Blogs(int page = 1)
        {
            var result = blogService.GetAll(page, 6);
            ViewBag.pageCount = (int)((result.Count() + 5) / 6);
            if (ViewBag.pageCount < page)
                return HttpNotFound();
            return View(result);
        }
        [HttpGet]
        public ActionResult NewBlog()
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(new Blog());
        }
        [HttpPost]
        public ActionResult NewBlog(Blog blog)
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(blog);
        }
        #endregion
    }
}