using Library.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]
    public class NewsCategoriesController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _newsImageFolder = "/Uploads/images/news";
        // GET: cms/NewsCategories
        public ActionResult Index()
        {
            return View(db.NewsCategories.ToList());
        }

        // GET: cms/NewsCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // GET: cms/NewsCategories/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(NewsCategory newsCategory, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                newsCategory.CreatedUserId = CurrentUser.AdminUserId;
                newsCategory.CategoryUrl = CommonHelper.BuildUrl(newsCategory.CategoryName);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _newsImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        newsCategory.ImageUrl = imageurl;
                    }
                }
                db.NewsCategories.Add(newsCategory);
                db.SaveChanges();
                return ReturnJson(result: 1);
            }
            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);
        }

        // GET: cms/NewsCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: cms/NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsCategory newsCategory, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                newsCategory.ModifiedUserId = CurrentUser.AdminUserId;
                newsCategory.CategoryUrl = CommonHelper.BuildUrl(newsCategory.CategoryName);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _newsImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        newsCategory.ImageUrl = imageurl;
                    }
                }
                db.Entry(newsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return ReturnJson(result: 1);
            }
            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);
        }

        // GET: cms/NewsCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategories.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: cms/NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsCategory newsCategory = db.NewsCategories.Find(id);
            db.NewsCategories.Remove(newsCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
