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

    public class CommonInfoesController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _imageFolder = "/Uploads/images";

        // GET: cms/CommonInfoes
        public ActionResult Index()
        {
            return View(db.CommonInfoes.ToList());
        }

        // GET: cms/CommonInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonInfo commonInfo = db.CommonInfoes.Find(id);
            if (commonInfo == null)
            {
                return HttpNotFound();
            }
            return View(commonInfo);
        }

        // GET: cms/CommonInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CommonInfo commonInfo, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                commonInfo.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, string.Empty, _imageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        commonInfo.FacebookImageUrl = imageurl;
                    }
                }
                db.CommonInfoes.Add(commonInfo);
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

        // GET: cms/CommonInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonInfo commonInfo = db.CommonInfoes.Find(id);
            if (commonInfo == null)
            {
                return HttpNotFound();
            }
            return View(commonInfo);
        }

        // POST: Cms/CommonInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(CommonInfo commonInfo, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                commonInfo.ModifiedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, string.Empty, _imageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        commonInfo.FacebookImageUrl = imageurl;
                    }
                }
                db.Entry(commonInfo).State = EntityState.Modified;
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

        // GET: cms/CommonInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonInfo commonInfo = db.CommonInfoes.Find(id);
            if (commonInfo == null)
            {
                return HttpNotFound();
            }
            return View(commonInfo);
        }

        // POST: cms/CommonInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommonInfo commonInfo = db.CommonInfoes.Find(id);
            db.CommonInfoes.Remove(commonInfo);
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
