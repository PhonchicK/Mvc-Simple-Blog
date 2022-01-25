using MvcSimpleBlog.Business.Abstract;
using MvcSimpleBlog.Entities.Concrete;
using MvcSimpleBlog.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Mvc;

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
        [Route("new")]
        public ActionResult Create()
        {
            return View(new Category());
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public ActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                categoryService.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Details(int id)
        {
            Category category = categoryService.GetById(id);
            if (category == null)
                return HttpNotFound();
            
            return View(category);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Details(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                Category updatingCategory = categoryService.GetById(id);
                if (updatingCategory == null)
                    return HttpNotFound();

                categoryService.Update(category);

                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            Category deletingCategory = categoryService.GetById(id);
            if (deletingCategory == null)
                return HttpNotFound();

            categoryService.Delete(deletingCategory);
            return RedirectToAction("Index");
        }
    }
}