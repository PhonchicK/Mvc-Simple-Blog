using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using MvcSimpleBlog.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Mvc;
using System.IO;

namespace MvcSimpleBlog.WebUI.Controllers
{
    [Auth]
    [RouteArea("admin")]
    [RoutePrefix("user")]
    public class AdminUserController : Controller
    {
        private IUserService userService;

        public AdminUserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("{page?}")]
        public ActionResult Index(int page = 1)
        {
            ViewBag.pageCount = (int)((userService.UserCount() + 9) / 10);
            return View(userService.GetAll(page));
        }

        [HttpGet]
        [Route("new")]
        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if((Session["user"] as User).Auth > user.Auth)
                    return View(user);

                userService.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Details(int id)
        {
            User user = userService.GetById(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Details(int id, User user)
        {
            if (ModelState.IsValid)
            {
                User updatingUser = userService.GetById(id);
                if (updatingUser == null)
                    return HttpNotFound();

                if ((Session["user"] as User).Auth <= updatingUser.Auth)
                    return View(user);

                userService.Update(user);

                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            User deletingUser = userService.GetById(id);
            if (deletingUser == null)
                return HttpNotFound();

            userService.Delete(deletingUser);
            return RedirectToAction("Index");
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