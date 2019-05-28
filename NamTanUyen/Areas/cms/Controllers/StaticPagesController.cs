using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;
using Library.Helper;
using NamTanUyen.Filters;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]

    public class StaticPagesController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _imageFolder = "/Uploads/images/staticpage";
        // GET: cms/StaticPages
        public ActionResult Index()
        {
            return View(db.StaticPages.ToList());
        }

        // GET: cms/StaticPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

        // GET: cms/StaticPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cms/StaticPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticPage staticPage, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, string.Empty, _imageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        staticPage.ImageUrl = imageurl;
                    }
                }
                staticPage.PageUrl = CommonHelper.BuildUrl(staticPage.PageName);
                staticPage.CreatedUserId = CurrentUser.AdminUserId;
                db.StaticPages.Add(staticPage);
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

        // GET: cms/StaticPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

        // POST: cms/StaticPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(StaticPage staticPage, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, string.Empty, _imageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        staticPage.ImageUrl = imageurl;
                    }
                }
                staticPage.PageUrl = CommonHelper.BuildUrl(staticPage.PageName);
                staticPage.ModifiedUserId = CurrentUser.AdminUserId;
                db.Entry(staticPage).State = EntityState.Modified;
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

        // GET: cms/StaticPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

        // POST: cms/StaticPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaticPage staticPage = db.StaticPages.Find(id);
            db.StaticPages.Remove(staticPage);
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
