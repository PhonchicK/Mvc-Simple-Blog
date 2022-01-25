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
        #region Login
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
        #endregion
        /*#region Blogs
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
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult NewBlog(Blog blog, HttpPostedFileBase Image)
        {
            if(ModelState.IsValid)
            {
                string imagePath = "/Content/Home/img/nullimage.jpg";
                if (Image != null)
                {
                    if (Image.ContentLength > 5 * 1024 * 1024 || Path.GetExtension(Image.FileName) != ".jpg")
                    {
                        return View(blog);
                    }
                    imagePath = "/Content/Uploads/" + blog.SeoUrl + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(
                                   Server.MapPath("~/Content/Uploads"), blog.SeoUrl + Path.GetExtension(Image.FileName)));
                }
                blog.Image = imagePath;
                blog.UserId = (Session["user"] as User).Id;
                blog.Date = DateTime.Now;
                blogService.Add(blog);

                return RedirectToAction("Blogs");
            }
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(blog);
        }

        [HttpGet]
        public ActionResult DetailsBlog(int id)
        {
            Blog blog = blogService.GetById(id);
            if (blog == null)
                return HttpNotFound();

            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name", blog.Category);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DetailsBlog(Blog blog, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                Blog updateBlog = blogService.GetById(blog.Id);
                if (updateBlog == null)
                    return HttpNotFound();

                blog.Date = updateBlog.Date;
                blog.UserId = updateBlog.UserId;

                blog.Image = updateBlog.Image;
                if (Image != null)
                {
                    if (Image.ContentLength > 5 * 1024 * 1024 || Path.GetExtension(Image.FileName) != ".jpg")
                    {
                        return View(blog);
                    }
                    Image.SaveAs(Path.Combine(
                                   Server.MapPath("~/Content/Uploads"), blog.SeoUrl + Path.GetExtension(Image.FileName)));

                    blog.Image = "/Content/Uploads/" + blog.SeoUrl + Path.GetExtension(Image.FileName);
                }
                blogService.Update(blog);
            }

            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name", blog.Category);
            return View(blog);
        }

        public ActionResult DeleteBlog(int id)
        {
            Blog blog = blogService.GetById(id);

            if (blog == null)
                return HttpNotFound();
            if (blog.User.Auth < (Session["user"] as User).Auth)
                return RedirectToAction("Blogs");

            blogService.Delete(blog);
            return RedirectToAction("Blogs");
        }
        #endregion*/
    }
}