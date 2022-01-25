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
using System.IO;


namespace MvcSimpleBlog.WebUI.Controllers
{
    [Auth]
    [RouteArea("admin")]
    [RoutePrefix("")]
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
        [Route("")]
        [Route("index")]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
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
        [Route("login")]
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
    }
}