using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using MvcSimpleBlog.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSimpleBlog.WebUI.Controllers
{
    [Auth]
    [RouteArea("admin")]
    [RoutePrefix("blog")]
    [Route("{action = Index}")]
    public class AdminBlogController : Controller
    {
        private IBlogService blogService;
        private ICategoryService categoryService;
        public AdminBlogController(IBlogService _blogService, ICategoryService _categoryService)
        {
            this.blogService = _blogService;
            this.categoryService = _categoryService;
        }
        [Route]
        public ActionResult Index(int page = 1)
        {
            return View(blogService.GetAll(page, 6));
        }

        [HttpGet, Route("new")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(new Blog());
        }

        [HttpPost, Route("new")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]//For CKEditor error
        public ActionResult Create(HttpPostedFileBase image, Blog blog)
        {
            if (ModelState.IsValid)
            {
                string imagePath = "/Content/Home/img/nullimage.jpg";
                ImageUpload(image, blog.SeoUrl, ref imagePath);
                blog.Image = imagePath;

                blog.UserId = (Session["user"] as User).Id;
                blog.Date = DateTime.Now;
                blogService.Add(blog);

                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(blog);
        }

        [HttpGet, Route("edit/{id}")]
        public ActionResult Details(int id)
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(blogService.GetById(id));
        }

        [HttpPost, Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]//For CKEditor error
        public ActionResult Details(int id, HttpPostedFileBase image, Blog blog)
        {
            if (ModelState.IsValid)
            {
                Blog updateBlog = blogService.GetById(id);

                if (updateBlog != null)
                {
                    string imagePath = blog.Image;
                    ImageUpload(image, blog.SeoUrl, ref imagePath);
                    blog.Image = imagePath;

                    blog.UserId = updateBlog.Id;
                    blog.Date = updateBlog.Date;
                    blog.Id = id;

                    blogService.Update(blog);

                   return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(blog);
        }

        #region General Functions
        private bool ImageUpload(HttpPostedFileBase image, string name, ref string imageLocation)
        {
            if (image != null)
            {
                if (image.ContentLength > 5 * 1024 * 1024 || Path.GetExtension(image.FileName) != ".jpg")
                {
                    return false;
                }
                try
                {
                    imageLocation = "/Content/Uploads/" + name + Path.GetExtension(image.FileName);
                    image.SaveAs(Path.Combine(
                                   Server.MapPath("~/Content/Uploads"), name + Path.GetExtension(image.FileName)));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}